namespace POS.AdPressWareHouseWorkSpace.Helper
{
    public enum UnitBuy
    {
        kilogram,
        liter
    }

    public enum UnitContain
    {
        gram,
        mililiter
    }


    public class UnitBuyTrans
    {
        public static double ToUnitContain(string unitBuy)
        {
            if (unitBuy.Equals(UnitBuy.kilogram.ToString()))
            {
                return 1000;
            }

            if (unitBuy.Equals(UnitBuy.liter.ToString()))
            {
                return 1000;
            }

            return 0;
        }
    }
}
