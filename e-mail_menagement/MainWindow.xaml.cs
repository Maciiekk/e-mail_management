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

namespace e_mail_menagement
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void SeparateDataFromEmail(string email)
        {
            email = email.ToLower();
            email=email.Trim();
            int atIndex=email.IndexOf("@"); 
           if(atIndex>0 && atIndex<email.Length)
            {
                int dotIndex;
                // separaete e-mail in to 2 substrings     beforeAt @ postAt
                string beforeAt = email.Substring(0, atIndex-1);
                dotIndex = beforeAt.IndexOf(".");
                
                string name = beforeAt.Substring(0, dotIndex);
                name = char.ToUpper(name[0]) +name.Substring(1);
                LabelName.Content = "Name: "+name;

                string surename =email.Substring(dotIndex+1,(atIndex-dotIndex)-1);
                surename = char.ToUpper(surename[0]) + surename.Substring(1);
                LabelSurename.Content ="Surename: "+surename;
                
                string postAt = email.Substring(atIndex+1);
                dotIndex=postAt.IndexOf(".");
    
                LabelDepartment.Content = "Department: " + postAt.Substring(0, dotIndex);
                int nextDotIndex = postAt.IndexOf('.', dotIndex+1); //how meany chars is between dots -1 .dev.  next= 2
                LabelJob.Content = "Job title: " + postAt.Substring(dotIndex+1, nextDotIndex-dotIndex-1 );
            }
        }

        private bool IsThatEmailCorrect(string email)
        {
            if(String.IsNullOrWhiteSpace(email)) 
            {
                LabelWarning.Content = "Warning: this is not a valid email address.\n Field is empty ";
                return false;
            }

            email = email.Trim();
            email = email.ToLower();

            if (!email.Contains('@')) 
            {
                LabelWarning.Content = "Warning: this is not a valid email address.\nMissing \"@\" ";
                return false;
            }

            int countDots = email.Count(f => (f == '.'));
           if (countDots != 4)
            {
                LabelWarning.Content = "Warning: this is not a valid email address.\nInvalid  format ";
                return false;
            }
            int dotIndex = email.IndexOf('.');

            int x = email.IndexOf('.', dotIndex + 1);
            if (email.IndexOf('.', dotIndex+1) < email.IndexOf('@'))
            {
                LabelWarning.Content = "Warning: this is not a valid email address.\nInvalid  format ";
                return false;
            }
     
            if(email.Substring(email.Length-9)!=".comp.com")
            {
                LabelWarning.Content = "Warning: this is not a valid email address.\n It is not a company email ";
                return false;
            }

            LabelWarning.Content = " ";
            return true;
        }

        private void ManageEmail(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
               if( IsThatEmailCorrect(textBoxEmail.Text))
                SeparateDataFromEmail(textBoxEmail.Text);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsThatEmailCorrect(textBoxEmail.Text))
                SeparateDataFromEmail(textBoxEmail.Text);
        }
    }
}
