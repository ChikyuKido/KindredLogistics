using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VampireCommandFramework;

namespace KindredLogistics.Commands.Converters;

public record struct Quantity(int quantity = 1,int quantityPercent = -1);

class QuantityConverter : CommandArgumentConverter<Quantity>
{
    public override Quantity Parse(ICommandContext ctx, string input)
    {
        int quantity = 0;
        if (int.TryParse(input, out quantity))
        {
            return new Quantity(quantity,-1);
        }

        string[] args = input.Split("%");
        if (args.Length != 2)
        {
            throw ctx.Error("Value does not contain a percent or a number: "+input);
        }
        if (args[1].Count() != 0)
        {
            throw ctx.Error("Value is not in a valid format: " + input);
        }
        int quantityPercent = 0;
        if (int.TryParse(args[0], out quantityPercent))
        {
            return new Quantity(-1, quantityPercent);
        }

        throw ctx.Error("Value is not in a valid format: " + input);
    }
}
