namespace POS.AdPressWareHouseWorkSpace.Helper
{
    public enum UnitOut
    {
        pcs
    }

    public enum UnitIn
    {
        pcs
    }


    public class UnitOutTrans
    {
        public static double ToUnitContain(string unitOut)
        {
            if (unitOut.Equals(UnitOut.pcs.ToString()))
            {
                return 1;
            }

            return 0;
        }
    }

    public class UnitInTrans
    {
        public static double ToUnitContain(string unitIn)
        {
            if (unitIn.Equals(UnitIn.pcs.ToString()))
            {
                return 1;
            }

            return 0;
        }
    }
}
