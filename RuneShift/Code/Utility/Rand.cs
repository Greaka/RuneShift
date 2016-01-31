using System;
using System.Diagnostics;

class Rand
{
    //random object with seed init
    //and deterministic sequence
    public Rand(uint _seed)
    {
        num = _seed;
    }

    public uint next(uint max, uint min = 0)
    {
        num ^= num << 13;
        num ^= num >> 17;
        num ^= num << 5;
        return (num % (max + 1 - min)) + min;
    }

    uint num;

    static Random random = new Random();

    /// <summary>random Value in Range [0.0, 1.0]</summary>
    public static float Value()
    {
        return (float)random.NextDouble();
    }

    /// <summary>random Value in Range [0.0, maxValue]</summary>
    public static float Value(float maxValue)
    {
        return (float)(random.NextDouble() * maxValue);
    }

    /// <summary>random Value in Range [minValue, maxValue]</summary>
    public static float Value(float minValue, float maxValue)
    {
        return (float)(minValue + random.NextDouble() * (maxValue - minValue));
    }

    /// <summary>random nonNegative number</summary>
    public static int IntValue()
    {
        return random.Next();
    }

    /// <summary>random nonNegative number, less than maxValue</summary>
    public static int IntValue(int maxValue)
    {
        return random.Next(maxValue);
    }

    /// <summary>random number, within Range</summary>
    public static int IntValue(int minValue, int maxValue)
    {
        return random.Next(minValue, maxValue);
    }
}

