using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace FrameWork.Message
{
    public class Value<T>
    {
        public Value(T value)
        {
            this.value = value;
        }


        T value;
        Func<T, bool> conditions;
        Func<T, T> filter;
        Action<T> Change;
        public T Get { get { return value; } }
        public void Set(T value)
        {
            if(conditions != null)
                if (!conditions(value)) return;

            this.value = (filter == null) ? value : filter(value);
            Change?.Invoke(value);
        }

        public void AddListner(Action<T> listner)
        {
            Change += listner;
        }
        public void RemoveListner(Action<T> listner)
        {
            Change -= listner;
        }

        public void SetConditions(Func<T, bool> conditions)
        {
            this.conditions = conditions;
        }
        public void RemoveConditions()
        {
            conditions = null;
        }
        
        public void SetFilter(Func<T, T> filter)
        {
            this.filter = filter;
        }
        public void RemoveFilter()
        {
            filter = null;
        }
    }
}
