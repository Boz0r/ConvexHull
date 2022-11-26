using System.Numerics;
using AutoFixture;
using BenchmarkDotNet.Attributes;
using ConvexHull.Algorithms;

namespace ConvexHull.Benchmark;

public class AlgorithmBenchmarks
{
    private readonly IConvexHullAlgorithm incrementalAlgorithm = new IncrementalAlgorithm();
    private readonly IConvexHullAlgorithm naiveAlgorithm = new NaiveAlgorithm();
    private readonly IConvexHullAlgorithm kirkpatrickSeidelAlgorithm = new KirkpatrickSeidelAlgorithm();

    private Dictionary<int, List<Vector3>> Data_Uniform = new();
    private Dictionary<int, List<Vector3>> Data_Circle = new();
    private Dictionary<int, List<Vector3>> Data_BoundingBox = new();


    public AlgorithmBenchmarks()
    {
        Data_Uniform[10] = new Fixture().CreateMany<Vector3>(10).ToList();
        Data_Uniform[100] = new Fixture().CreateMany<Vector3>(100).ToList();
        Data_Uniform[1000] = new Fixture().CreateMany<Vector3>(1000).ToList();
        Data_Uniform[10000] = new Fixture().CreateMany<Vector3>(10000).ToList();       
        
        Data_Circle[10] = new Fixture().Build<Vector3>().FromFactory((float delta) => ToPolarCoordinates(1000, delta)).CreateMany(10).ToList();
        Data_Circle[100] = new Fixture().Build<Vector3>().FromFactory((float delta) => ToPolarCoordinates(1000, delta)).CreateMany(100).ToList();
        Data_Circle[1000] = new Fixture().Build<Vector3>().FromFactory((float delta) => ToPolarCoordinates(1000, delta)).CreateMany(1000).ToList();
        Data_Circle[10000] = new Fixture().Build<Vector3>().FromFactory((float delta) => ToPolarCoordinates(1000, delta)).CreateMany(10000).ToList();
        
        Data_BoundingBox[10] = new Fixture().CreateMany<Vector3>(10).ToList();
        Data_BoundingBox[100] = new Fixture().CreateMany<Vector3>(100).ToList();
        Data_BoundingBox[1000] = new Fixture().CreateMany<Vector3>(1000).ToList();
        Data_BoundingBox[10000] = new Fixture().CreateMany<Vector3>(10000).ToList();
        
        Data_BoundingBox[10].AddRange(new List<Vector3>
        {
            new(3.40282347E+30f, 3.40282347E+30f, 0),
            new(-3.40282347E+30f, 3.40282347E+30f, 0), 
            new(3.40282347E+30f, -3.40282347E+30f, 0),
            new(-3.40282347E+30f, -3.40282347E+30f, 0),
        });
        Data_BoundingBox[100].AddRange(new List<Vector3>
        {
            new(3.40282347E+30f, 3.40282347E+30f, 0),
            new(-3.40282347E+30f, 3.40282347E+30f, 0), 
            new(3.40282347E+30f, -3.40282347E+30f, 0),
            new(-3.40282347E+30f, -3.40282347E+30f, 0),
        });
        Data_BoundingBox[1000].AddRange(new List<Vector3>
        {
            new(3.40282347E+30f, 3.40282347E+30f, 0),
            new(-3.40282347E+30f, 3.40282347E+30f, 0), 
            new(3.40282347E+30f, -3.40282347E+30f, 0),
            new(-3.40282347E+30f, -3.40282347E+30f, 0),
        });
        Data_BoundingBox[10000].AddRange(new List<Vector3>
        {
            new(3.40282347E+30f, 3.40282347E+30f, 0),
            new(-3.40282347E+30f, 3.40282347E+30f, 0), 
            new(3.40282347E+30f, -3.40282347E+30f, 0),
            new(-3.40282347E+30f, -3.40282347E+30f, 0),
        });
    }

    [Benchmark, Arguments(10), Arguments(100), Arguments(1000), Arguments(10000)]
    public List<Vector3> KirkpatrickSeidelAlgorithm_Uniform(int count)
    {
        var input = Data_BoundingBox[count];
        return kirkpatrickSeidelAlgorithm.Compute(input);
    }
    
    [Benchmark, Arguments(10), Arguments(100), Arguments(1000), Arguments(10000)]
    public List<Vector3> IncrementalAlgorithm_Uniform(int count)
    {
        var input = Data_BoundingBox[count];
        return incrementalAlgorithm.Compute(input);
    }
    
    [Benchmark, Arguments(10), Arguments(100), Arguments(1000), Arguments(10000)]
    public List<Vector3> KirkpatrickSeidelAlgorithm_Circle(int count)
    {
        var input = Data_Circle[count];
        return kirkpatrickSeidelAlgorithm.Compute(input);
    }
    
    [Benchmark, Arguments(10), Arguments(100), Arguments(1000), Arguments(10000)]
    public List<Vector3> IncrementalAlgorithm_Circle(int count)
    {
        var input = Data_Circle[count];
        return incrementalAlgorithm.Compute(input);
    }
    
    [Benchmark, Arguments(10), Arguments(100), Arguments(1000), Arguments(10000)]
    public List<Vector3> KirkpatrickSeidelAlgorithm_BoundingBox(int count)
    {
        var input = Data_BoundingBox[count];
        return kirkpatrickSeidelAlgorithm.Compute(input);
    }
    
    [Benchmark, Arguments(10), Arguments(100), Arguments(1000), Arguments(10000)]
    public List<Vector3> IncrementalAlgorithm_BoundingBox(int count)
    {
        var input = Data_BoundingBox[count];
        return incrementalAlgorithm.Compute(input);
    }
    
    // [Benchmark, Arguments(10), Arguments(100), Arguments(1000)]
    public List<Vector3> NaiveAlgorithm(int count)
    {
        var input = new Fixture().CreateMany<Vector3>(count).ToList();
        return naiveAlgorithm.Compute(input);
    }

    public Vector3 ToPolarCoordinates(float r, float delta)
    {
        return new Vector3(r * MathF.Cos(delta), r * MathF.Sin(delta), 0);
    }
}
