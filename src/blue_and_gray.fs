// Set the pixel color to blue or gray depending on is_moon.
//
// Uniforms:
uniform bool is_moon;
// Outputs:
out vec3 color;
void main()
{
  /////////////////////////////////////////////////////////////////////////////
  // Replace with your code:

  color = int(is_moon) * vec3(0.451, 0.4, 0.455) + (1 - int(is_moon)) * vec3(0.208, 0.243, 0.784);

  /////////////////////////////////////////////////////////////////////////////
}
