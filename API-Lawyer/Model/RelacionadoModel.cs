namespace API_Lawyer.Model
{
    public class RelacionadoModel
    {
        private String Id { get; set; }
        private enum Tipo { Apensos, Vinculados }

        private String Descricao { get; set; }
        private DateTime Data { get; set; }
        private String IdNumeroProcesso { get; set; }




    }
}
