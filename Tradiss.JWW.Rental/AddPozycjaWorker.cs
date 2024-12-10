
using Soneta.Business;
using Soneta.Handel;
using Soneta.Types;
using Soneta.Zadania;
using Tradiss.JWW.Rental;
[assembly:Worker(typeof(AddPozycjaWorker),typeof(Urzadzenie))]

namespace Tradiss.JWW.Rental
{
    public class AddPozycjaWorker
    {
        private Urzadzenie _urzadzenie;
        private readonly PozycjaPars pars;

        public AddPozycjaWorker(Urzadzenie urzadzenie,PozycjaPars pars)
        {
            this._urzadzenie = urzadzenie;
            this.pars = pars;
        }
        [Action("Przypisz pozycję.",Target = ActionTarget.ToolbarWithText)]
        public void Run()
        {
            this._urzadzenie.PozycjaDokHandlowego = pars.Pozycja;
        }

    }
    public class PozycjaPars : ContextBase
    {
        public PozycjaPars(Context context) : base(context){}
        [Caption("Wybierz pozycje")]
        public PozycjaDokHandlowego Pozycja { get; set; }
        public object GetListPozycja()
        {
            Context.Get(out Urzadzenie urzadzenie);
            if (urzadzenie?.DokumentHandlowy is null)
                return null;

            var pozycje = HandelModule.GetInstance(Session).PozycjeDokHan.WgDokument[urzadzenie.DokumentHandlowy].CreateView();
            return new LookupInfo.EnumerableItem("Pozycje dokumentu", pozycje, new[] {"PełnaNazwa" })
            {
                ComboBox = false,
                
                
            };
        }
    }
}
