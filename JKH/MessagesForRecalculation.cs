using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace JKH
{
    public class MessagesForRecalculation
    {
        public MessagesForRecalculation(Street street, Home home, Flat flat, Consumer consumer, float allDolg, Rent rent)
        {
            MailAddress From = new MailAddress("EGGspert@yandex.ru", "Чехов Игорь Павлович");
            MailAddress To = new MailAddress("EGGspert@yandex.ru");
            MailMessage message = new MailMessage(From, To);

            message.Subject = "Запрос на перерасчёт";
            string mda = "<h1 align='Center'>Запрос на перерасчёт</h1><h3> Лицевой счёт: " + consumer.PersonalAccount.ToString() + "</h3><h3> Пользователь: " + consumer.Surname.ToString() + " " + consumer.Name.ToString() + " " + consumer.Patronymic.ToString() + "</h3><h4> Проживающий по адресу: Улица " + street.selectedStreetName.ToString() + ", Дом " + home.SelectedNumberHome + ", Квартира " + flat.SelectedNumberFlat + "</h4><h4> Счёт составляет: " + Math.Round(allDolg) + " рублей</h4><h4> Оплачено: " + Math.Round(rent.PaidUp) + " рублей</h4><h3> Не оплачено: " + (Math.Round(allDolg) - Math.Round(rent.PaidUp)) + "</h3>";

            message.Body = mda;
            message.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587);
            smtp.Credentials = new NetworkCredential("EGGspert@yandex.ru", "Tions3120771");
            smtp.EnableSsl = true;
            smtp.Send(message);
        }
    }
}
