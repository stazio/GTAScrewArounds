using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NativeUI;
using System.Windows.Forms;

namespace Staz_GTA_Screws
{
    class UI
    {
        private NativeUI.UIMenu menu;
        private MenuPool pool;

        private Dictionary<UIMenuItem, Delegate> delegates;
        
       
        public UI()
        {
            menu = new UIMenu("Staz'z Screw Thingies", "Just some stupid things!");
            pool = new MenuPool();
            pool.Add(menu);

            delegates = new Dictionary<UIMenuItem, Delegate>();
            AddButton("Kill Everyone", Actions.KILL_ALL);
            AddButton("Kill Everyone Repedatly",  Actions.REPEATING_KILL_ALL);

            AddButton("Explode Everything", Actions.EXPLODE_ALL);
            AddButton("Explode Everything Repeatadly", Actions.REPEATING_EXPLOSION);

            AddButton("Make Self Invincible", Actions.PLAYER_INVINCIBLE);
            AddButton("Make Targated Invincible", Actions.TARGET_INVINCIBLE);

            AddButton("Remove All Entities", Actions.REMOVE_ENTITIES);
            AddButton("Spawn Car", Actions.SPAWN_CAR);
            menu.OnItemSelect += OnClick;

        }

        public void OnKeyDown(object o, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
              menu.Visible = !menu.Visible;

            pool.ProcessKey(e.KeyCode);

        }

        public void OnTick(object sender, EventArgs e)
        {
            // if (menu.Visible)
            //       menu.Draw();
            pool.ProcessMenus();
            Actions.Tick(sender, e);
        }

        private void AddButton(String text, OnClickEvent lambda)
        {
            UIMenuItem item = new UIMenuItem(text);
            this.delegates.Add(item, lambda);
            menu.AddItem(item);
        }

        private void AddButton(String text, ToggleEvent toggleD)
        {
            UIMenuItem item = new UIMenuItem(text);
            
            this.delegates.Add(item, toggleD);
            this.props.Add(item, false);
            
            menu.AddItem(item);
        }

        private void OnClick(UIMenu sender, UIMenuItem item, int index)
        {
            Delegate del = this.delegates[item];
            if (del is OnClickEvent)
                ((OnClickEvent)del)();
            else if (del is ToggleEvent)
                ((ToggleEvent)del)(toggle(item));
        }
        
        private Dictionary<UIMenuItem, Boolean> props = new Dictionary<UIMenuItem, Boolean>();
        private bool toggle(UIMenuItem item)
        {
            if (!props.ContainsKey(item))
                props[item] = false;
            
            if (!props[item])
            {
                item.SetLeftBadge(UIMenuItem.BadgeStyle.Tick);
            }
            else
            {
                item.SetLeftBadge(UIMenuItem.BadgeStyle.None);
            }
            props[item] = !props[item];
            return props[item];
            
        }
    }

    delegate void OnClickEvent();
    delegate void ToggleEvent(Boolean toggle);
}
