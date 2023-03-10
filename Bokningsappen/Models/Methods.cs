namespace Bokningsappen.Models
{
    public class Methods
    {
        enum MainMenu
        {
            Show_Rooms = 1,
            Book_Rooms,
            Trending_Rooms,
            Admin
        }
        enum AdminMenu
        {
            Add_Room = 1,
            Remove_Room,
            Cancel_Room,
            Main_Menu,
        }
        public static void RunMe()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Mohammed's Booking app" + "\n" + "Please choose one of the options below:  ");
            foreach(int c in Enum.GetValues(typeof(MainMenu)))
            {
                Console.WriteLine($"{c}: " +$"{Enum.GetName(typeof(MainMenu), c).Replace('_', ' ')}");
            }

            MainMenu mainmenu = (MainMenu)99;
            int number;
            if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out number))
            {
                mainmenu = (MainMenu)number;
                Console.Clear();
            }
            switch (mainmenu)
            {
                case MainMenu.Show_Rooms:
                    ShowAllRoomsStatus();
                    Console.ReadKey();
                    break;  
                case MainMenu.Book_Rooms:
                    BookRoom();
                    break;
                case MainMenu.Trending_Rooms:
                    ShowTrendingRooms();
                    break;
                case MainMenu.Admin:
                    Admin();
                    //AdminCheck();
                    break;
            }
        }
        //Login funkar om man skriver rätt inlogg, men funkar ej när man skriver fel.
        //public static void AdminCheck()
        //{
        //    var db = new BokingAppContext();

        //    Console.WriteLine("Verify you self, intruder!!");
            
        //    Console.Write("Username: ");
        //    string username = Console.ReadLine();
        //    Console.Write("Password: ");
        //    string password = Console.ReadLine();


        //    var AdminP = db.Admins.Where(p => p.Password == password).SingleOrDefault();    
        //    var AdminU = db.Admins.Where(u => u.UserName == username).SingleOrDefault();


        //    if(username == AdminU.UserName && password == AdminP.Password)
        //    {
        //        Console.WriteLine("Correct information as given, you will be directed to the admin menu");
        //        Thread.Sleep(2000);
        //        Admin();
        //    }
        //    else if (username != AdminU.UserName || password != AdminP.Password)
        //    {
        //        Console.WriteLine("That is not the correct username or password, try again looser!");
        //        Thread.Sleep(2000);
        //        AdminCheck();
        //    }

        //}
        public static void Admin()
        {
            var db = new BokingAppContext();

            Console.Clear();
            Console.WriteLine("Welcome to the admin side of Mohammeds's Booking app" + "\n" + "Please choose one of the options below:  ");
            foreach (int c in Enum.GetValues(typeof(AdminMenu)))
            {
                Console.WriteLine($"{c}: " + $"{Enum.GetName(typeof(AdminMenu), c).Replace('_', ' ')}");
            }

            AdminMenu adminmenu = (AdminMenu)99;
            int number;
            if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out number))
            {
                adminmenu = (AdminMenu)number;
                Console.Clear();
            }
            switch (adminmenu)
            {
                case AdminMenu.Add_Room:
                    CreateRoomAdmin();
                    break;
                case AdminMenu.Remove_Room:
                    RemoveRoomAdmin();
                    break;
                case AdminMenu.Cancel_Room:
                    CancelRoomAdmin();
                    break;
                case AdminMenu.Main_Menu:
                    RunMe();
                    break;
            }
        }
        public static void CreateRooms()
        {
            var db = new BokingAppContext();

            Room r = new Room()
            {
                RoomName = "Whale",
                Booked = false,
            };
            db.Rooms.Add(r);
            db.SaveChanges();
        }
        public static void ShowAllRoomsStatus()
        {
            bool run = true;
            //while (run == true)
            //{
                var db = new BokingAppContext();
                Console.WriteLine("[RoomID]" + $"\t" + "[RoomName]" + $"\t" + "[RoomSize]" +  $"\t" + "[Status]");
                foreach (var room in db.Rooms)
                {
                    Console.WriteLine(room.ID + "\t\t" + room.RoomName + "\t\t" + room.RoomSize + "\t\t" + room.Booked);
                }

                Console.WriteLine("Press X to go back to the menu");
                string quit = Console.ReadLine();
                if (quit == "X" || quit == "x")
                {
                    RunMe();
                }
                else
                {
                    Console.WriteLine("Please press X");

                }
           // }
        }
        public static void ShowAllRooms()
        {
            var db = new BokingAppContext();
            Console.WriteLine("[RoomID]" + $"\t" + "[RoomName]" + $"\t" + "[RoomSize]" + $"\t" + "[Status]");
            foreach (var room in db.Rooms)
            {
                Console.WriteLine(room.ID + "\t\t" + room.RoomName + "\t\t" + room.RoomSize + "\t\t" + room.Booked);
            }
        }
        public static void BookRoom()
        {
            Console.Clear();
            var db = new BokingAppContext();
            ShowAllRooms();

            Console.WriteLine("What room would you like to book?");

            string userinput = Console.ReadLine();


            int roomId;
            bool UpdateBookedStatus = true;

            if (int.TryParse(userinput, out roomId))
            {
                var LookingForRooms = (from rooms in db.Rooms
                                       where rooms.ID == roomId
                                       select rooms).SingleOrDefault();

                if (LookingForRooms != null && LookingForRooms.Booked == false)
                {
                    Console.WriteLine("Under what name would you like to book this room?: ");
                    string ChosenRoomName = Console.ReadLine();

                    var FindRoom = db.Rooms.Where(r => r.ID == roomId).SingleOrDefault();

                    FindRoom.Name = ChosenRoomName;
                    FindRoom.ID= roomId;
                    FindRoom.Booked = UpdateBookedStatus;
                    FindRoom.popularity++;
                    FindRoom.BookedDate = DateTime.Now;

                    db.SaveChanges();
                    Console.WriteLine($"You have successfully booked room {FindRoom.RoomName} under the name {FindRoom.Name}");
                    RunMe();

                }
                else if (LookingForRooms != null && LookingForRooms.Booked == true)
                {
                    Console.WriteLine("That room is already booked");
                    Thread.Sleep(1500);
                    BookRoom();
                }
                else if (LookingForRooms == null)
                {
                    Console.WriteLine("We dont have such a room, try another room");
                }
            }

        }
        public static void CreateRoomAdmin()
        {
            var db = new BokingAppContext();

            Console.WriteLine("What would you like to call the room?");
            string UIRoomName = Console.ReadLine();

            Console.WriteLine("What is the room size?");
            int UIRoomSize = int.Parse(Console.ReadLine());

            bool BookedStatus = false;
            var newRoom = new Room
            {
                RoomName = UIRoomName,
                RoomSize = UIRoomSize,
                Booked = BookedStatus,
            };
           
            db.Rooms.Add(newRoom);
            db.SaveChanges();
            Console.WriteLine($"You have successfully added {UIRoomName} as a room into the system!");
            Thread.Sleep(1000);
            Admin();
        }
        public static void RemoveRoomAdmin()
        {
            var db = new BokingAppContext();

            Console.WriteLine("[RoomID]" + $"\t" + "[RoomName]" + $"\t" + "[Status]");
            foreach (var room in db.Rooms)
            {
                Console.WriteLine(room.ID + $"\t\t" + room.RoomName + "\t\t" + room.Booked);
            }

            Console.WriteLine("What room would you like to remove? [Enter RoomID]");
            var ChosenRoomID = int.Parse(Console.ReadLine());

            var ChosenRoom = db.Rooms.Where(r => r.ID == ChosenRoomID).FirstOrDefault();

            if (ChosenRoom.Booked == true)
            {
                Console.WriteLine($"You can't remove {ChosenRoom.RoomName}, becuase it's booked." + $"\nYou need to cancel it first");
                Thread.Sleep(2000);
                RemoveRoomAdmin();
            }
            else
            {
                db.Remove(ChosenRoom);
                db.SaveChanges();

                Console.WriteLine($"You have successfully added {ChosenRoom.RoomName} as a room into the system!");
                Thread.Sleep(2000);
                Admin();
            }
        }
        public static void CancelRoomAdmin()
        {
            Console.Clear();
            ShowAllRooms();

            var db = new BokingAppContext();

            Console.WriteLine("What room would you like to cancel? [Enter RoomID]");
            var ChosenRoomID = int.Parse(Console.ReadLine());

       
            var ChosenRoom = db.Rooms.Where(r => r.ID == ChosenRoomID).FirstOrDefault();
            if (ChosenRoom.Booked == true)
            {
                ChosenRoom.Name = null;
                ChosenRoom.Booked = false;
                ChosenRoom.BookedDate = null;
                db.SaveChanges();

                Console.WriteLine($"You have successfully canceld {ChosenRoom.RoomName}");
                Thread.Sleep(2000);
                RunMe();
            }
            else if (ChosenRoom.Booked == false)
            {
                Console.WriteLine("This room isnt booked, please try and cancel a already booked room!");
                Thread.Sleep(2000);
                CancelRoomAdmin();
            }
          
            Admin();
        }
        public static void AddAdmin()
        {
            var db = new BokingAppContext();


            Admin a = new Admin()
            {
                Name = "Mohammed Karimi",
                UserName = "Admin123",
                Password = "Admin123",
            };

            db.Admins.Add(a);
            db.SaveChanges();
        }
        public static void ShowTrendingRooms()
        {
            var db = new BokingAppContext();
            Console.Clear();
            var PopRoom = (from p in db.Rooms
                           where p.popularity >= 0
                           orderby p.popularity descending
                           select p);

            Console.WriteLine("[Popularity]" + $"\t" + "[RoomName]" + $"\t" + "[RoomSize]" + $"\t" + "[Status]");
            foreach (var room in PopRoom)
            {
                Console.WriteLine(room.popularity + "\t\t" + room.RoomName + "\t\t" + room.RoomSize + "\t\t" + room.Booked);
            }
            Console.WriteLine("Press X if you want to go back to the main menu");

            string UserInput = Console.ReadLine();

            if (UserInput == "X" || UserInput == "x")
            {
                RunMe();
            }
            else
            {
                Console.WriteLine("Please enter a vaild key");
                Thread.Sleep(2000);
                ShowTrendingRooms();
            }
        }
    }
}
