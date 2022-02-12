using System.Collections.Generic;

namespace R365.Models
{
    public class ExpressionDto
    {
        public IEnumerable<int> Components { get; private set; }

        public ExpressionDto(IEnumerable<int> components)
        {
            this.Components = components;
        }
    }
}
