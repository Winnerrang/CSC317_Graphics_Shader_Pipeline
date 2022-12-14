// Set the pixel color to an interesting procedural color generated by mixing
// and filtering Perlin noise of different frequencies.
//
// Uniforms:
uniform mat4 view;
uniform mat4 proj;
uniform float animation_seconds;
uniform bool is_moon;
// Inputs:
in vec3 sphere_fs_in;
in vec3 normal_fs_in;
in vec4 pos_fs_in; 
in vec4 view_pos_fs_in; 
// Outputs:
out vec3 color;

// expects: blinn_phong, perlin_noise
void main()
{
  /////////////////////////////////////////////////////////////////////////////
  // Replace with your code 
  float theta = 0.25 * M_PI * animation_seconds;

  vec4 lightPos = view * vec4(-4 * sin(theta), 4, 4 * cos(theta), 1);

  vec3 l = normalize((lightPos - view_pos_fs_in).xyz);

  vec3 n = normalize(normal_fs_in);

  vec3 v = -normalize((view_pos_fs_in).xyz);

  vec3 ka = vec3(0.05, 0.05, 0.05);

  vec3 kd = int(is_moon) * vec3(0.451, 0.4, 0.455) + (1 - int(is_moon)) * vec3(0.208, 0.243, 0.784);

  vec3 ks = vec3(1, 1, 1);

  float p = 500 * int(!is_moon) + 500;

  // different value will create different texture
  float twist_factor = int(is_moon) * 50 + (1-int(is_moon)) * 11;
  float density = int(is_moon) * 3 + (1-int(is_moon)) * 10;
  float weight = 6;
  float noise = sqrt(abs(1 + sin(sphere_fs_in.x + twist_factor * perlin_noise(density * sphere_fs_in))/weight));

  //float texture = 10.0 + 11.0 * int(is_moon);
  //float amplitude = 0.2 + 0.1 * int(is_moon);
  //kd = kd * (1 + amplitude * perlin_noise(texture * sphere_fs_in));
  color = blinn_phong(ka, noise * kd, ks, p, n, v, l);
 
  /////////////////////////////////////////////////////////////////////////////
}
