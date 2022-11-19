// Add (hard code) an orbiting (point or directional) light to the scene. Light
// the scene using the Blinn-Phong Lighting Model.
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
// expects: PI, blinn_phong
void main()
{
  /////////////////////////////////////////////////////////////////////////////
  // Replace with your code 
  // calculate light

  float theta = 0.25 * M_PI * animation_seconds;

  vec4 lightPos = view * vec4(-4 * sin(theta), 4, 4 * cos(theta), 1);

  vec3 l = normalize((lightPos - view_pos_fs_in).xyz);

  vec3 n = normalize(normal_fs_in);

  vec3 v = -normalize((view_pos_fs_in).xyz);

  vec3 ka = vec3(0.05, 0.05, 0.05);

  vec3 kd = int(is_moon) * vec3(0.451, 0.4, 0.455) + (1 - int(is_moon)) * vec3(0.208, 0.243, 0.784);

  vec3 ks = vec3(1, 1, 1);

  float p = 1000;

  color = blinn_phong(ka, kd, ks, p, n, v, l);

  /////////////////////////////////////////////////////////////////////////////
}
