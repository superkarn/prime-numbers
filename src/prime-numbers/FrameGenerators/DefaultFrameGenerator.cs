using System;
using System.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace prime_numbers.FrameGenerators
{
    public class DefaultFrameGenerator : IFrameGenator
    {
        int width;
        int height;
        int[] data;

        public DefaultFrameGenerator(int width = 100, int height = 100, int[] data = null)
        {
            this.width = width;
            this.height = height;
            this.data = data;
        }

        public Image<Rgba32> Generate(int wrapWidth)
        {
            // wrapWidth must be <= width
            if (wrapWidth > this.width)
            {
                return null;
            }

            var image = new Image<Rgba32>(this.width, this.height);

            // x, y coordinate of the pixels
            var x = 0;
            var y = 0;

            // Get the largest number from data
            var maxNumber = this.data[this.data.Length-1];

            // The number of pixels in this image
            var maxPixelCount = this.width * this.height;

            // Loop until we run out of prime numbers or pixels
            for (var ii = 0; ii < maxNumber && ii < maxPixelCount && y < this.height; ii++)
            {
                // Prime numbers
                if (this.data.Contains(ii))
                {
                    image[x, y] = Colors.BLUE;
                }
                // Non prime numbers 
                else
                {
                    // TODO Optimize drawing the grid
                    // Color Grid
                         if (x % 100 == 0) image[x, y] = Colors.GRID100;
                    else if (y % 100 == 0) image[x, y] = Colors.GRID100;
                    else if (x %  10 == 0) image[x, y] = Colors.GRID10;
                    else if (y %  10 == 0) image[x, y] = Colors.GRID10;
                    else                   image[x, y] = Colors.WHITE;
                }
                
                // Move the pixel left to right, then top to bottom
                if (x > wrapWidth-2) 
                {
                    x = 0;
                    y++;
                } 
                else 
                {
                    x++;
                }
            }
            
            return image;
        }
    }
}