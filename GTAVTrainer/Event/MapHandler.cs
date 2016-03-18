using GTA;
using GTA.Math;
using GTAVTrainer.Net.Data;

namespace GTAVTrainer.Event
{
    public class MapHandler : EventHandler
    {
        public MapHandler(Script script) : base(script)
        {
            Dictionary.Add("get_markers", GetMarkers);
            Dictionary.Add("teleport", Teleport);
        }

        public EventData GetMarkers(EventData readEventData)
        {
            var writeEventData = new EventData("map", "set_markers");

            var playerPosition = Game.Player.Character.Position;
            writeEventData.AddData("player", new Data()
                .AddData("vector2", new Data()
                    .AddFloat("x", playerPosition.X)
                    .AddFloat("y", playerPosition.Y))
                .AddString("street", World.GetStreetName(playerPosition))
                .AddString("zone", World.GetZoneName(playerPosition)));

            var waypointPosition = World.GetWaypointPosition();
            if (waypointPosition.X != 0.0f && waypointPosition.Y != 0.0f)
            {
                writeEventData.AddData("waypoint", new Data()
                    .AddData("vector2", new Data()
                        .AddFloat("x", waypointPosition.X)
                        .AddFloat("y", waypointPosition.Y))
                    .AddString("street", World.GetStreetName(waypointPosition))
                    .AddString("zone", World.GetZoneName(waypointPosition)));
            }

            return writeEventData;
        }

        public EventData Teleport(EventData readEventData)
        {
            var vector2Data = readEventData.GetData("vector2");
            var x = vector2Data.GetFloat("x");
            var y = vector2Data.GetFloat("y");

            var ped = Game.Player.Character;
            Entity entity = ped;
            if (ped.IsInVehicle())
            {
                entity = ped.CurrentVehicle;
            }

            var z = 1000.0f;
            for (var i = 0; i <= 1000; i += 50)
            {
                entity.Position = new Vector3(x, y, i);

                GTA.Script.Wait(100);

                var groundHeight = World.GetGroundHeight(new Vector2(x, y));
                if (groundHeight > 0.0f)
                {
                    z = groundHeight;
                    break;
                }
            }

            entity.Position = new Vector3(x, y, z);

            return null;
        }
    }
}