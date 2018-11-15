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
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace Makra {
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        List<Vybaveni> vybaveni = new List<Vybaveni>();
        List<Atributy> atribut = new List<Atributy>();

        string file = File.ReadAllText("../../makro1.txt");
        string file2 = File.ReadAllText("../../makro2.txt");
        string file3 = File.ReadAllText("../../makro3.txt");

        public MainWindow() {
            InitializeComponent();
            Makra_edit();
        }

        void Makra_edit() {
            tretitext.Text = file;
            prvnitext.Text = file2;
            druhejtext.Text = file3;
        }

        async void makro1(object sender, RoutedEventArgs e) {
            var state = await CSharpScript.RunAsync<int>(file);
            Vysledek.Content = state.ReturnValue;
            tretitext.Text = file;
        }

        async void makro1a(object sender, RoutedEventArgs e) {
            var state = await CSharpScript.RunAsync<int>(tretitext.Text);
            Vysledek.Content = state.ReturnValue;
        }

        async void makro2(object sender, RoutedEventArgs e) {
            Globals globals = new Globals();
            globals.VybaveniInGlobals = new Vybaveni();
            globals.AtributyInGlobals = new Atributy();
            var metadata = MetadataReference.CreateFromFile(typeof(Globals).Assembly.Location);
            await CSharpScript.RunAsync(file2 , options: ScriptOptions.Default.WithReferences(metadata) , globals: globals);

            Vybaveni vybav = new Vybaveni();
            vybav.Predmet = globals.VybaveniInGlobals.Predmet;
            vybav.Cena = globals.VybaveniInGlobals.Cena;
            Atributy atrib = new Atributy();
            atrib.Inteligence = globals.AtributyInGlobals.Inteligence;
            atrib.Sila = globals.AtributyInGlobals.Sila;
            atrib.Zdravi = globals.AtributyInGlobals.Zdravi;

            predmet.Content = globals.VybaveniInGlobals.Predmet;
            cena.Content = globals.VybaveniInGlobals.Cena;

            intel.Content = globals.AtributyInGlobals.Inteligence;
            sila.Content = globals.AtributyInGlobals.Sila;
            zdravi.Content = globals.AtributyInGlobals.Zdravi;

            vybaveni.Add(vybav);
            atribut.Add(atrib);
            nasob.Visibility = Visibility.Visible;
            prvnitext.Text = file2;
            druhejtext.Text = file3;
        }

        async void makro2a(object sender, RoutedEventArgs e) {
            Globals globals = new Globals();
            globals.VybaveniInGlobals = new Vybaveni();
            globals.AtributyInGlobals = new Atributy();
            var metadata = MetadataReference.CreateFromFile(typeof(Globals).Assembly.Location);
            await CSharpScript.RunAsync(prvnitext.Text, options: ScriptOptions.Default.WithReferences(metadata), globals: globals);

            Vybaveni vybav = new Vybaveni();
            vybav.Predmet = globals.VybaveniInGlobals.Predmet;
            vybav.Cena = globals.VybaveniInGlobals.Cena;
            Atributy atrib = new Atributy();
            atrib.Inteligence = globals.AtributyInGlobals.Inteligence;
            atrib.Sila = globals.AtributyInGlobals.Sila;
            atrib.Zdravi = globals.AtributyInGlobals.Zdravi;

            predmet.Content = globals.VybaveniInGlobals.Predmet;
            cena.Content = globals.VybaveniInGlobals.Cena;

            intel.Content = globals.AtributyInGlobals.Inteligence;
            sila.Content = globals.AtributyInGlobals.Sila;
            zdravi.Content = globals.AtributyInGlobals.Zdravi;

            vybaveni.Add(vybav);
            atribut.Add(atrib);
            nasob.Visibility = Visibility.Visible;
        }

        async void makro3(object sender, RoutedEventArgs e) {
            var state3 = await CSharpScript.RunAsync<double>(file3);

            intel.Content =  atribut[0].Inteligence * state3.ReturnValue;
            sila.Content = atribut[0].Sila * state3.ReturnValue;
            zdravi.Content = atribut[0].Zdravi * state3.ReturnValue;

            cena.Content = vybaveni[0].Cena * state3.ReturnValue;

            atribut[0].Inteligence = Convert.ToInt32(intel.Content);
            atribut[0].Sila = Convert.ToInt32(sila.Content);
            atribut[0].Zdravi = Convert.ToInt32(zdravi.Content);

            vybaveni[0].Cena = Convert.ToInt32(cena.Content);
            prvnitext.Text = file2;
            druhejtext.Text = file3;
        }

        async void makro3a(object sender, RoutedEventArgs e) {
            var state3 = await CSharpScript.RunAsync<double>(druhejtext.Text);

            intel.Content = atribut[0].Inteligence * state3.ReturnValue;
            sila.Content = atribut[0].Sila * state3.ReturnValue;
            zdravi.Content = atribut[0].Zdravi * state3.ReturnValue;

            cena.Content = vybaveni[0].Cena * state3.ReturnValue;

            atribut[0].Inteligence = Convert.ToInt32(intel.Content);
            atribut[0].Sila = Convert.ToInt32(sila.Content);
            atribut[0].Zdravi = Convert.ToInt32(zdravi.Content);

            vybaveni[0].Cena = Convert.ToInt32(cena.Content);
        }
    }
}
