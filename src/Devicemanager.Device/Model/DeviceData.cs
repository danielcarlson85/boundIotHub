using System.Collections.Generic;

namespace Bound
{
    public class DeviceData
    {
        public string MachineName { get; set; }
        public string ObjectId { get; set; }
        public List<TrainingData> TrainingData { get; set; }
    }

    public class TrainingData
    {
        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }
    }
}
