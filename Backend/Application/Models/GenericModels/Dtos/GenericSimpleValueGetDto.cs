using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.GenericModels.Dtos
{
    public class GenericSimpleValueGetDto<T>
    {
        private readonly T value;
        public GenericSimpleValueGetDto(T value)
        {
            this.value = value;
        }

        public T ReturnedValue => this.value;
    }
}
