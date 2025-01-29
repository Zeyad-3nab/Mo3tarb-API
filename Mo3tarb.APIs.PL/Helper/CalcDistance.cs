namespace Mo3tarb.APIs.PL.Helper
{
    public static class CalcDistance
    {
        public static double CalculateDistance(double lat2, double lon2)
        {
            double lat1 = 30.575191777601496;
            double lon1 = 31.008882986858065;


            const double earthRadiusKm = 6371;

            double dlat = TransDegreeToRedius(lat2 - lat1);
            double dlon = TransDegreeToRedius(lon2 - lon1);

            double a = Math.Sin(dlat / 2) * Math.Sin(dlat / 2) +
                Math.Cos(TransDegreeToRedius(lat1)) *
                Math.Cos(TransDegreeToRedius(lat2)) *
                Math.Sin(dlon / 2) * Math.Sin(dlon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return earthRadiusKm * 1000 * c;
        }

        public static double TransDegreeToRedius(double degree)
        {
            return degree * Math.PI / 180;
        }
    }
}
