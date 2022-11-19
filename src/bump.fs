// Set the pixel color using Blinn-Phong shading (e.g., with constant blue and
// gray material color) with a bumpy texture.
// 
// Uniforms:
uniform mat4 view;
uniform mat4 proj;
uniform float animation_seconds;
uniform bool is_moon;
// Inputs:
//                     linearly interpolated from tessellation evaluation shader
//                     output
in vec3 sphere_fs_in;
in vec3 normal_fs_in;
in vec4 pos_fs_in; 
in vec4 view_pos_fs_in; 
// Outputs:
//               rgb color of this pixel
out vec3 color;
// expects: model, blinn_phong, bump_height, bump_position,
// improved_perlin_noise, tangent
void main()
{
  /////////////////////////////////////////////////////////////////////////////
  // Replace with your code 
	  float theta = 0.25 * M_PI * animation_seconds;

	  vec4 lightPos = view * vec4(-4 * sin(theta), 4, 4 * cos(theta), 1);

	  vec3 l = normalize((lightPos - view_pos_fs_in).xyz);

	  vec3 v = -normalize((view_pos_fs_in).xyz);

	  vec3 ka = vec3(0.05, 0.05, 0.05);

	  vec3 kd = int(is_moon) * vec3(0.451, 0.4, 0.455) + (1 - int(is_moon)) * vec3(0.208, 0.243, 0.784);

	  vec3 ks = vec3(1, 1, 1);

	  float p = 500 * int(!is_moon) + 500;

	  vec3 T, B;
	  tangent(sphere_fs_in, T, B);
	  float delta = 1e-5;
	  vec3 bump = bump_position(is_moon, sphere_fs_in);

	  vec3 dp_dt = (bump_position(is_moon, sphere_fs_in + delta * T)-bump)/delta;
	  vec3 dp_db = (bump_position(is_moon, sphere_fs_in + delta * B)-bump)/delta;

	  vec3 new_n = normalize(cross(dp_dt, dp_db));
	  theta = 0.5 * M_PI * animation_seconds;
	  vec3 n = normalize((transpose(inverse(view)) * rotate_about_y(theta) * vec4(new_n, 1.0)).xyz);

	  color = blinn_phong(ka, kd, ks, p, n, v, l);
  //color = vec3(1,1,1);
  /////////////////////////////////////////////////////////////////////////////
}
