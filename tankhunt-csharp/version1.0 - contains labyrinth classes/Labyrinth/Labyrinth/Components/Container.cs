using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Labyrinth
{
    public class Container
    {
        public Labyrinth Labyrinth { get; set; }
        public PlayerTankComponent Player_tank_c { get; set;}
        public NetworkComponent Network_c { get; set; }
        public SimpleRandomLevelComponent Srl_c {get; set;}
        public TcpSpriteDrawer Tcp_sprite_drawer_c { get; set; }
        public DisconnectComponent Disconnect_c { get; set; }
        public MShotsComponent Mshots_c { get; set; }
        public ExceptionComponent Exception_c { get; set; }
        public ItemsComponent Items_c { get; set; }
        public ScreenInterfaceComponent Screen_in_c { get; set; }

    }
}
