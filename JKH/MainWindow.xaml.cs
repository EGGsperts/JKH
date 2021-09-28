using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JKH
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SQLPrebludi sqlPr = new SQLPrebludi();
        public Consumer consumer { get; set; }
        public Rent RentMode { get; set; }
        public Home HomeMode { get; set; }
        public Flat FlatMode { get; set; }
        public Street StreetMode { get; set; }
        public NeedYear NeedYearMode { get; set; }
        public NeedMonth NeedMonthMode { get; set; }
        public DebtorVal debtorAll { get; set; }
        public float DolgForMonth { get; set; }
        public float DolgAll { get; set; }
        public string AllMonth { get; set; }
        
        public MainWindow()
        {
            InitializeComponent();

            sqlPr.getConnection();
            StreetMode = sqlPr.TakeStreet(sqlPr.TakeNameStreet());
            foreach (string name in StreetMode.nameStreet)
            {
                Lez.Items.Add(name);
            }
            NeedYearMode = sqlPr.TakeYear(sqlPr.TakeNameYear());
            foreach (int name in NeedYearMode.AllYear)
            {
                ListYear.Items.Add(name);
            }
            NeedMonthMode = sqlPr.TakeMonth(sqlPr.TakeNameMonth());
            foreach (string name in NeedMonthMode.AllMonth)
            {
                ListMonth.Items.Add(name);
            }
        }
        void DebtorInfo(DebtorVal debtorInfo)
        {
            float doljok;
            float info = 0;
            string month = null;
            for (int i = 0; i < debtorInfo.DebtorRent.Count; i++)
            {
                if (month == null)
                {
                    month = debtorInfo.DebtorRent[i].NeededMonth;
                }
                else 
                {
                    month = month + ", " + debtorInfo.DebtorRent[i].NeededMonth;
                }

                doljok = debtorInfo.DebtorRent[i].ColdWaterSupply + debtorInfo.DebtorRent[i].HotWaterSupply + debtorInfo.DebtorRent[i].WaterDisposal + debtorInfo.DebtorRent[i].Heating + debtorInfo.DebtorRent[i].PowerSupply + debtorInfo.DebtorRent[i].SolidMunicipalWasteManagement + debtorInfo.DebtorRent[i].GasSupply;
                info = info + (float)(Math.Round(doljok) - Math.Round(debtorInfo.DebtorRent[i].PaidUp));
            }
            DebtorMonth.Content = month;
            DebtorAll.Content = info + " ₽";
            DolgAll = info ;
            AllMonth = month;
        }
        private void Lez_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

            if (Lez.SelectedItem != null)
            {
                if (labAdress.Content.ToString().Contains("Д.") && labAdress.Content.ToString().Contains("Ул."))
                {
                    if (ListYear.SelectedItem != null && ListMonth.SelectedItem != null)
                    {
                        sqlPr.TakeAccount(sqlPr.TakePersonalAccount(Lez.SelectedItem.ToString(),HomeMode.SelectedNumberHome));
                        consumer = sqlPr.TakeConsumer(sqlPr.TakeConsumerInfo(sqlPr.Account));
                        RentMode = sqlPr.TakeRent(sqlPr.TakeRentInfo(sqlPr.Account, NeedYearMode.SelectedYear, NeedMonthMode.SelectedMonth));
                        debtorAll = sqlPr.TakeDebtor(sqlPr.TakeDebtorRent(sqlPr.Account, NeedYearMode.SelectedYear));
                        AccountNumber.Content = sqlPr.Account;
                        FlatMode.SelectedNumberFlat = Convert.ToInt32(Lez.SelectedItem);

                        RealName.Content = consumer.Name;
                        RealFam.Content = consumer.Surname;
                        RealPat.Content = consumer.Patronymic;

                        ColdWater.Content = RentMode.ColdWaterSupply + " ₽";
                        HotWater.Content = RentMode.HotWaterSupply + " ₽";
                        WaterOut.Content = RentMode.WaterDisposal + " ₽";
                        Otop.Content = RentMode.Heating + " ₽";
                        PowerLight.Content = RentMode.PowerSupply + " ₽";
                        Musor.Content = RentMode.SolidMunicipalWasteManagement + " ₽";
                        Gas.Content = RentMode.GasSupply + " ₽";
                        NameMonth.Content = RentMode.NeededMonth;
                        NameYear.Content = RentMode.NeededYear;
                        DolgForMonth = RentMode.ColdWaterSupply + RentMode.HotWaterSupply + RentMode.WaterDisposal + RentMode.Heating + RentMode.PowerSupply + RentMode.SolidMunicipalWasteManagement + RentMode.GasSupply;
                        SUD.IsEnabled = true;
                        Replace.IsEnabled = true;
                        Debtor.Content = Math.Round(DolgForMonth) - Math.Round(RentMode.PaidUp) + " ₽";
                        Itog.Content = DolgForMonth.ToString() + " ₽";
                        DebtorInfo(debtorAll);
                    }
                    else
                    {
                        MessageBox.Show("Выберите год и месяц!");
                    }
                }
                else if (labAdress.Content.ToString().Contains("Ул."))
                {
                    labAdress.Content = labAdress.Content + ", Д." + Lez.SelectedItem.ToString();
                    FlatMode = sqlPr.TakeFlat(sqlPr.TakeNumberFlat(Lez.SelectedItem.ToString()));
                    
                    HomeMode.SelectedNumberHome = Convert.ToInt32(Lez.SelectedItem);
                    Lez.Items.Clear();
                    foreach (int number in FlatMode.NumberFlat)
                    {
                        Lez.Items.Add(number);
                    }
                }
                else
                {
                    labAdress.Content = "Ул." + Lez.SelectedItem.ToString();
                    HomeMode = sqlPr.TakeHome(sqlPr.TakeNumberHome(Lez.SelectedItem.ToString()));
                    
                    StreetMode.selectedStreetName = Lez.SelectedItem.ToString();
                    Lez.Items.Clear();
                    foreach (int number in HomeMode.NumberHome)
                    {
                        Lez.Items.Add(number);
                    }
                }
            }
        }

        private void arrowBack_Click(object sender, RoutedEventArgs e)
        {
            if (labAdress.Content.ToString().Contains("Д.") && labAdress.Content.ToString().Contains("Ул."))
            {
                Lez.Items.Clear();
                labAdress.Content = "Ул." + StreetMode.selectedStreetName;
                HomeMode = sqlPr.TakeHome(sqlPr.TakeNumberHome(StreetMode.selectedStreetName));
                SUD.IsEnabled = false;
                Replace.IsEnabled = false;
                Lez.Items.Clear();
                foreach (int number in HomeMode.NumberHome)
                {
                    Lez.Items.Add(number);
                }
            }
            else if (labAdress.Content.ToString().Contains("Ул."))
            {
                Lez.Items.Clear();
                labAdress.Content = "Все улицы";
                StreetMode = sqlPr.TakeStreet(sqlPr.TakeNameStreet());
                SUD.IsEnabled = false;
                Replace.IsEnabled = false;
                foreach (string name in StreetMode.nameStreet)
                {
                    Lez.Items.Add(name);
                }
            }
            else
            {
                MessageBox.Show("Не куда больше идти!");
            }
            RealName.Content = null;
            RealFam.Content = null;
            RealPat.Content = null;
            AccountNumber.Content = null;
            ColdWater.Content = null;
            HotWater.Content = null;
            WaterOut.Content = null;
            Otop.Content = null;
            PowerLight.Content = null;
            Musor.Content = null;
            Gas.Content = null;
            NameMonth.Content = null;
            NameYear.Content = null;
            Itog.Content = null;
            Debtor.Content = null;
            DebtorMonth.Content = null;
            DebtorAll.Content = null;
        }

        private void SUD_Click(object sender, RoutedEventArgs e)
        {
            MessagesForShutdown hoba = new MessagesForShutdown(StreetMode, HomeMode, FlatMode, consumer, DolgForMonth,RentMode,AllMonth,DolgAll.ToString());
            MessageBox.Show("Сообщение отправлено");
        }

        private void ListYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NeedYearMode.SelectedYear = Convert.ToInt32(ListYear.SelectedItem);
        }

        private void ListMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NeedMonthMode.SelectedMonth = ListMonth.SelectedItem.ToString();
        }

        private void Replace_Click(object sender, RoutedEventArgs e)
        {
            MessagesForRecalculation hoba = new MessagesForRecalculation(StreetMode, HomeMode, FlatMode, consumer, DolgForMonth, RentMode);
            MessageBox.Show("Сообщение отправлено");
        }
    }
}
