using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day20
{
    internal class Program
    {
        internal class Particle : IEquatable<Particle>
        {
            public (int, int, int) Position { get; private set; }
            public (int, int, int) Velocity { get; private set; }
            public (int, int, int) Acceleration { get; }

            public Particle((int, int, int) position, (int, int, int) velocity, (int, int, int) acceleration)
            {
                Position = position;
                Velocity = velocity;
                Acceleration = acceleration;
            }

            public void Tick()
            {
                Velocity = (Velocity.Item1 + Acceleration.Item1, Velocity.Item2 + Acceleration.Item2, Velocity.Item3 + Acceleration.Item3);
                Position = (Position.Item1 + Velocity.Item1, Position.Item2 + Velocity.Item2, Position.Item3 + Velocity.Item3);
            }

            public int Manhattan() => Math.Abs(Position.Item1) + Math.Abs(Position.Item2) + Math.Abs(Position.Item3);

            public override string ToString() => $"p={Position}, v={Velocity}, a={Acceleration}";

            public bool Equals(Particle other)
            {
                if (other is null)
                    return false;
                return ReferenceEquals(this, other) || Position.Equals(other.Position);
            }

            public override bool Equals(object obj)
            {
                if (obj is null)
                    return false;
                if (ReferenceEquals(this, obj))
                    return true;
                return obj.GetType() == GetType() && Equals((Particle)obj);
            }

            public override int GetHashCode() => Position.GetHashCode();
        }

        private static void Main(string[] args)
        {
            new Program().SolvePart2();
            Console.ReadLine();
        }

        private Program()
        {

        }

        private void SolvePart1()
        {
            var input = LoadInput();
            var particleRegex = new Regex(@"p=<((?:(?:-?\d+),?)+)>, v=<((?:(?:-?\d+),?)+)>, a=<((?:(?:-?\d+),?)+)>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var particles = new List<Particle>();

            foreach (var line in input)
            {
                var match = particleRegex.Match(line);
                var posString = match.Groups[1].Value;
                var velString = match.Groups[2].Value;
                var accelString = match.Groups[3].Value;

                var posArgs = posString.Split(',');
                var pos = (int.Parse(posArgs[0]), int.Parse(posArgs[1]), int.Parse(posArgs[2]));

                var velArgs = velString.Split(',');
                var vel = (int.Parse(velArgs[0]), int.Parse(velArgs[1]), int.Parse(velArgs[2]));

                var accelArgs = accelString.Split(',');
                var accel = (int.Parse(accelArgs[0]), int.Parse(accelArgs[1]), int.Parse(accelArgs[2]));

                var particle = new Particle(pos, vel, accel);
                //Console.WriteLine($"Created new particle: {particle}");
                particles.Add(particle);
            }

            //for (var i = 0; i < 150_000; i++)
            //{
            //    foreach (var particle in particles)
            //        particle.Tick();
            //}

            var closest = particles.Select((p, i) => (p, i)).OrderBy(p => Math.Abs(p.Item1.Acceleration.Item1) + Math.Abs(p.Item1.Acceleration.Item2) + Math.Abs(p.Item1.Acceleration.Item3)).First();
            Console.WriteLine($"Closest: {closest.Item1} @ {closest.Item2}");
        }

        private void SolvePart2()
        {
            var input = LoadInput();
            var particleRegex = new Regex(@"p=<((?:(?:-?\d+),?)+)>, v=<((?:(?:-?\d+),?)+)>, a=<((?:(?:-?\d+),?)+)>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var particles = (from line in input
                             select particleRegex.Match(line) into match
                             let posString = match.Groups[1].Value
                             let velString = match.Groups[2].Value
                             let accelString = match.Groups[3].Value
                             let posArgs = posString.Split(',')
                             let pos = (int.Parse(posArgs[0]), int.Parse(posArgs[1]), int.Parse(posArgs[2]))
                             let velArgs = velString.Split(',')
                             let vel = (int.Parse(velArgs[0]), int.Parse(velArgs[1]), int.Parse(velArgs[2]))
                             let accelArgs = accelString.Split(',')
                             let accel = (int.Parse(accelArgs[0]), int.Parse(accelArgs[1]), int.Parse(accelArgs[2]))
                             select new Particle(pos, vel, accel)).ToList();

            Console.WriteLine($"Particles: {particles.Count}");

            for (var i = 0; i < 50; i++)
            {
                foreach (var p in particles)
                    p.Tick();

                var duplicates = (from p in particles
                    group p by p
                    into grouped
                    where grouped.Count() > 1
                    select grouped.Key).ToList();

                if (!duplicates.Any())
                    continue;

                foreach (var dup in duplicates)
                    particles.RemoveAll(p => p.Equals(dup));
            }

            Console.WriteLine($"Remaining particles: {particles.Count}");
        }

        private string[] LoadInput() => File.ReadAllLines("input.txt");

        private string[] SampleInput1() => new[] {
            "p=<3,0,0>, v=<2,0,0>, a=<-1,0,0>",
            "p=<4,0,0>, v=<0,0,0>, a=<-2,0,0>"
        };
    }
}
