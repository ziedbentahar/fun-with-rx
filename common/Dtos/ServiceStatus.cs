namespace Common.Dtos
{
    public class ServiceStatus
    {
        public string ServiceId { get; set; }
        public string Status { get; set; }

        public override string ToString()
        {
            return $"Service status for {ServiceId} is {Status}";
        }

        public override bool Equals(object obj)
        {

            var item = obj as ServiceStatus;

            if (item == null)
            {
                return false;
            }

            return ServiceId == item.ServiceId && Status == item.Status;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + ServiceId.GetHashCode();
            hash = (hash * 7) + Status.GetHashCode();
            return hash;
        }



    }
}
