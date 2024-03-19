using System;
using System.Text.Json;

using Abdelfettah.Youbi.FeatureMatching;

public class Program
{
    public static async Task Main(string[] args)
    {
        if (args.Length < 2 || !File.Exists(args[0]) || !Directory.Exists(args[1]))
        {
            throw new ArgumentException("Usage: <path_to_object_image> <path_to_scene_images_directory>");
        }

        var imageData = await File.ReadAllBytesAsync(args[0]);
        List<byte[]> scenesData = new List<byte[]>();

        foreach(var sceneImagePath in Directory.EnumerateFiles(args[1]))
        {
            scenesData.Add(await File.ReadAllBytesAsync(sceneImagePath));
        }


        var detectObjectInScenesResults = await new ObjectDetection().DetectObjectInScenesAsync(imageData, scenesData);

        foreach (var objectDetectionResult in detectObjectInScenesResults)
        {
            Console.WriteLine($"Points: {JsonSerializer.Serialize(objectDetectionResult.Points)}");
        }
    }
}