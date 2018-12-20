using Server.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        private static Account account;


        public static void Main(string[] args)
        {
            Uri address = new Uri("http://localhost:4000/IService1");
            BasicHttpBinding binding = new BasicHttpBinding();

            EndpointAddress endpoint = new EndpointAddress(address);

            ChannelFactory<IService1> factory = new ChannelFactory<IService1>(binding, endpoint);

            IService1 channel = factory.CreateChannel();


            ConsoleKey command;

            while (true)
            {
                Console.Clear();
                account = channel.UpdateAccount();
                PrintHeader();
                PrintMenu();

                command = Console.ReadKey().Key;

                Console.Clear();
                switch (command)
                {
                    case ConsoleKey.D1:
                        MenuDeposit(channel);
                        break;
                    case ConsoleKey.D2:
                        MenuWithdraw(channel);
                        break;
                    case ConsoleKey.D3:
                        MenuBet(channel);
                        break;
                    case ConsoleKey.D4:
                        MenuReceiveBet(channel);
                        break;
                }
            }
        }


        private static void MenuDeposit(IService1 channel)
        {
            PrintDepositeMenu();
            float valueFloat = ReadFloat();

            if (channel.DepositMoney(account, valueFloat))
                PrintSuccess();
            else
                PrintError();

            Console.ReadKey(true);
        }

        private static void MenuWithdraw(IService1 channel)
        {
            PrintWithdrawMenu();
            float valueFloat = ReadFloat();

            if (channel.WithdrawMoney(account, valueFloat))
                PrintSuccess();
            else
                PrintError();

            Console.ReadKey(true);
        }

        private static void MenuReceiveBet(IService1 channel)
        {
            ConsoleKey commad;

            Console.WriteLine("1. Вывести ставку по ее id");
            Console.WriteLine("2. Вывести все ставки аккаунта");

            commad = Console.ReadKey(true).Key;

            if (commad == ConsoleKey.D1)
                ReceiveIdBet(channel);
            else if (commad == ConsoleKey.D2)
                ReceiveAccoutBet(channel);
        }

        private static void ReceiveIdBet(IService1 channel)
        {
            ConsoleKey command = ConsoleKey.D1;
            int id = 0;
            Bet bet;
            while (command == ConsoleKey.D1)
            {
                Console.WriteLine("Введите id: ");
                id = ReadInt();
                bet = channel.GetBetById(id);

                if (bet == null)
                    Console.WriteLine("Ставка не найдена");
                else
                    Console.WriteLine(bet.ToString());

                Console.WriteLine("Чтобы продолжить поиск, нажмите 1... \nДля выхода нажмите любую другую клавишу...");
                command = Console.ReadKey(true).Key;
            }
        }

        private static void ReceiveAccoutBet(IService1 channel)
        {
            List<Bet> bets = channel.GetBetsByAccount(account.Id);

            foreach (Bet b in bets)
            {
                Console.WriteLine("--------------------------------------------------------------------------");
                Console.WriteLine(b.ToString());
            }

            Console.WriteLine("Чтобы продолжить, нажмите любую клавишу...");
            Console.ReadKey(true);
        }

        private static float ReadFloat()
        {
            float value = 0;
            string temp = Console.ReadLine();
            try
            {
                value = Math.Abs(float.Parse(temp, CultureInfo.InvariantCulture.NumberFormat));
            } catch (FormatException e)
            {
                value = 0;
            }

            return value;
        }

        private static int ReadInt()
        {
            int value = 0;
            string temp = Console.ReadLine();
            try
            {
                value = Math.Abs(int.Parse(temp, CultureInfo.InvariantCulture.NumberFormat));
            }
            catch (FormatException e)
            {
                value = 0;
            }

            return value;
        }

        private static void MenuBet(IService1 channel)
        {
            float summary;
            float coefficient;
            bool result;
            char c;
          
            ConsoleKey command = ConsoleKey.D1;
            
            Bet bet = new Bet();

            Console.WriteLine("Меню создания ставки");
            
            while (command == ConsoleKey.D1)
            {
                Console.WriteLine("Добавить исход:");
                Console.WriteLine("Результат (Победа - 1, Поражение - любая клавиша): ");
                c = Console.ReadKey(true).KeyChar;

                result = c.Equals('1');

                Console.WriteLine("\nКоэффициент: ");
                coefficient = ReadFloat();

                bet.AddOutcome(new Outcome(result, coefficient));

                Console.WriteLine("Чтобы добавить еще один исход, нажмите клавишу 1...");
                command = Console.ReadKey(true).Key;
            }

            Console.WriteLine("Итоговый коэффициент: " + bet.Coefficient);

            Console.WriteLine("Сумма: ");
            summary = ReadFloat();
            bet.Summary = summary;

            Console.WriteLine("Возможный выигрыш: " + (bet.Summary * bet.Coefficient));

            if (channel.CreateBet(bet, account))
            {
                Console.WriteLine("Ставка принята.");
            }
            else
            {
                Console.WriteLine("Произошла ошибка.");
            }

            Console.WriteLine("Нажмите любую клавишу, чтобы продолжить...");
            Console.ReadKey();
        }

        private static void PrintMenu()
        {
            Console.WriteLine("1. Пополнить баланс");
            Console.WriteLine("2. Уменьшить баланс");
            Console.WriteLine("3. Сделать ставку");
            Console.WriteLine("4. Получить ставку");
        }

        private static void PrintSuccess()
        {
            Console.WriteLine("Операция прошла успешно");
            Console.WriteLine("Нажмите любую клавишу, чтобы продолжить...");
        }

        private static void PrintError()
        {
            Console.WriteLine("Возникла ошибка");
            Console.WriteLine("Нажмите любую клавишу, чтобы продолжить...");
        }

        private static void PrintDepositeMenu()
        {
            Console.WriteLine("Ваш баланс: " + account.Balance + "\n");
            Console.WriteLine("Сумма внесения: " );
        }

        private static void PrintWithdrawMenu()
        {
            Console.WriteLine("Ваш баланс: " + account.Balance + "\n");
            Console.WriteLine("Сумма c снятия: ");
        }

        private static void PrintHeader()
        {
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("" + account.LastName + " " + account.FirstName + " " + account.Patronymicl);
            Console.WriteLine("Ваш возраст: " + (DateTime.Now.Year - account.Birthday.Date.Year));
            Console.WriteLine("Ваш личный номер: " + account.Id + " Ваш баланс: " + account.Balance);
            Console.WriteLine("--------------------------------------------------------------------------");
        }
    }
}
