namespace AgroSolutions.IoT.IngestaoDados.Domain.Entities;

public sealed class LeituraSensorTalhao
    {
        public Guid Id { get; private set; }
        public string SensorId { get; private set; }
        public string TalhaoId { get; private set; }
        public string PropriedadeId { get; private set; }
        public string NomeTalhao { get; private set; }
        public string CulturaPlantada { get; private set; }
        public decimal AreaEmHectares { get; private set; }
        public DateTime Timestamp { get; private set; }
        public decimal UmidadeSoloPercentual { get; private set; }
        public decimal TemperaturaCelsius { get; private set; }
        public decimal PrecipitacaoMm { get; private set; }
        public decimal UmidadeArPercentual { get; private set; }
        public decimal VelocidadeVentoKmh { get; private set; }
        public DateTime CriadoEm { get; private set; }

        private LeituraSensorTalhao() { }

        public LeituraSensorTalhao(
            string sensorId,
            string talhaoId,
            string propriedadeId,
            string nomeTalhao,
            string culturaPlantada,
            decimal areaEmHectares,
            DateTime timestamp,
            decimal umidadeSoloPercentual,
            decimal temperaturaCelsius,
            decimal precipitacaoMm,
            decimal umidadeArPercentual,
            decimal velocidadeVentoKmh)
        {
            Id = Guid.NewGuid();
            SensorId = sensorId;
            TalhaoId = talhaoId;
            PropriedadeId = propriedadeId;
            NomeTalhao = nomeTalhao;
            CulturaPlantada = culturaPlantada;
            AreaEmHectares = areaEmHectares;
            Timestamp = timestamp;
            UmidadeSoloPercentual = umidadeSoloPercentual;
            TemperaturaCelsius = temperaturaCelsius;
            PrecipitacaoMm = precipitacaoMm;
            UmidadeArPercentual = umidadeArPercentual;
            VelocidadeVentoKmh = velocidadeVentoKmh;
            CriadoEm = DateTime.UtcNow;
        }
    }
