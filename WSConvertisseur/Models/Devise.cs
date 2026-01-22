namespace WSConvertisseur.Models
{
    public class Devise
    {
        int id;
        string nomdevise;
        double taux;

        public Devise()
        {
        }

        public Devise(int id, string nomdevise, double taux)
        {
            this.Id = id;
            this.Nomdevise = nomdevise;
            this.Taux = taux;
        }

        public int Id
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
            }
        }

        public string Nomdevise
        {
            get
            {
                return this.nomdevise;
            }

            set
            {
                this.nomdevise = value;
            }
        }

        public double Taux
        {
            get
            {
                return this.taux;
            }

            set
            {
                this.taux = value;
            }
        }

        // Je commente pour le commit
    }
}
