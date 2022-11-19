// Generate a procedural planet and orbiting moon. Use layers of (improved)
// Perlin noise to generate planetary features such as vegetation, gaseous
// clouds, mountains, valleys, ice caps, rivers, oceans. Don't forget about the
// moon. Use `animation_seconds` in your noise input to create (periodic)
// temporal effects.
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

	vec3 kd =  int(is_moon) * vec3(0.79, 0.79, 0.79) + (1-int(is_moon)) * vec3(0.56, 0.2, 0.51);

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

	float twist_factor = int(is_moon) * 50 + (1-int(is_moon)) * 11;
	float density = int(is_moon) * 3 + (1-int(is_moon)) * 10;
	float weight = 6;
	float noise = sqrt(abs(1 + sin(sphere_fs_in.x + twist_factor * improved_perlin_noise(density * sphere_fs_in))/weight));

	color = blinn_phong(noise *sin(5*theta) * ka, noise * kd, ks, p, n, v, l);

  /////////////////////////////////////////////////////////////////////////////
}
