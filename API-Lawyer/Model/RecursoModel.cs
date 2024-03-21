namespace API_Lawyer.Model
{
    public class RecursoModel
    {
        private String Id { get; set; }
        private enum Tipo { Incidentes, Acidentais, Recursos, Sentenças}
        private String Descricao { get; set; }
        private DateTime Data { get; set; }
        private String IdNumeroProceso { get; set; }




    }
}
