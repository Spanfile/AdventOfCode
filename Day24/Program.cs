using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Day24
{
    internal class Program
    {
        internal class Component : IEquatable<Component>
        {
            public int Link1 { get; }
            public int Link2 { get; }
            public int Strength => Link1 + Link2 + (link2Connection?.Strength ?? 0);

            private Component link1Connection;
            private Component link2Connection;
            
            public Component(string componentString)
            {
                var args = componentString.Split('/');
                Link1 = int.Parse(args[0]);
                Link2 = int.Parse(args[1]);
            }

            public bool CanConnectTo(Component component)
            {
                if (Link2 == component.Link1 && link2Connection == null && component.link1Connection == null)
                    return true;
                
                return Link1 == component.Link2 && link1Connection == null && component.link2Connection == null;
            }

            public bool Connect(Component component)
            {
                if (Link2 == component.Link1)
                {
                    if (link2Connection != null || component.link1Connection != null)
                        return false;

                    link2Connection = component;
                    component.link1Connection = this;
                    return true;
                }
                if (Link1 == component.Link2)
                {
                    if (link1Connection != null || component.link2Connection != null)
                        return false;
                    
                    link1Connection = component;
                    component.link2Connection = this;
                    return true;
                }
                if (Link1 == component.Link1)
                {
                    if (link1Connection != null || component.link1Connection != null)
                        return false;

                    link1Connection = component;
                    component.link1Connection = this;
                    return true;
                }
                if (Link2 == component.Link2)
                {
                    if (link2Connection != null || component.link2Connection != null)
                        return false;

                    link2Connection = component;
                    component.link2Connection = this;
                    return true;
                }

                return false;
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                sb.Append(Link1).Append("/").Append(Link2);

                if (link2Connection != null)
                    sb.Append("--").Append(link2Connection);

                return sb.ToString();
            }

            public bool Equals(Component other)
            {
                if (ReferenceEquals(null, other))
                    return false;
                if (ReferenceEquals(this, other))
                    return true;
                return Link1 == other.Link1 && Link2 == other.Link2;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                    return false;
                if (ReferenceEquals(this, obj))
                    return true;
                return obj.GetType() == GetType() && Equals((Component) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (Link1 * 397) ^ Link2;
                }
            }

            public Component Clone() => (Component) MemberwiseClone();
        }
        
        private static void Main(string[] args)
        {
            new Program().SolvePart1();
            Console.ReadLine();
        }

        private Program()
        {

        }

        private void SolvePart1()
        {
            var input = SampleInput1();
            
        }

        private string[] LoadInput() => File.ReadAllLines("input.txt");

        private string[] SampleInput1() => new[]
        {
            "0/2",
            "2/2",
            "2/3",
            "3/4",
            "3/5",
            "0/1",
            "10/1",
            "9/10"
        };
    }
}
