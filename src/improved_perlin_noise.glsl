// Given a 3d position as a seed, compute an even smoother procedural noise
// value. "Improving Noise" [Perlin 2002].
//
// Inputs:
//   st  3D seed
// Values between  -½ and ½ ?
//
// expects: random_direction, improved_smooth_step
float improved_perlin_noise( vec3 st) 
{
  /////////////////////////////////////////////////////////////////////////////
  // Replace with your code 
  vec3 corners[8];
  corners[0] = vec3(floor(st.x), floor(st.y), floor(st.z));
  corners[1] = vec3(ceil(st.x), floor(st.y), floor(st.z));
  corners[2] = vec3(ceil(st.x), ceil(st.y), floor(st.z));
  corners[3] = vec3(floor(st.x), ceil(st.y), floor(st.z));
  corners[4] = vec3(floor(st.x), floor(st.y), ceil(st.z));
  corners[5] = vec3(ceil(st.x), floor(st.y), ceil(st.z));
  corners[6] = vec3(ceil(st.x), ceil(st.y), ceil(st.z));
  corners[7] = vec3(floor(st.x), ceil(st.y), ceil(st.z));

  //get all gradient of 8 corners
  vec3 gradient[8];
  gradient[0] = random_direction(corners[0]);
  gradient[1] = random_direction(corners[1]);
  gradient[2] = random_direction(corners[2]);
  gradient[3] = random_direction(corners[3]);
  gradient[4] = random_direction(corners[4]);
  gradient[5] = random_direction(corners[5]);
  gradient[6] = random_direction(corners[6]);
  gradient[7] = random_direction(corners[7]);

  //get the value for each corner
  float beforeInterpolate[8];
  beforeInterpolate[0] = dot(st - corners[0], gradient[0]);
  beforeInterpolate[1] = dot(st - corners[1], gradient[1]);
  beforeInterpolate[2] = dot(st - corners[2], gradient[2]);
  beforeInterpolate[3] = dot(st - corners[3], gradient[3]);
  beforeInterpolate[4] = dot(st - corners[4], gradient[4]);
  beforeInterpolate[5] = dot(st - corners[5], gradient[5]);
  beforeInterpolate[6] = dot(st - corners[6], gradient[6]);
  beforeInterpolate[7] = dot(st - corners[7], gradient[7]);

  //interpolate points between two point along x axis
  vec3 steps = improved_smooth_step(st - corners[0]);

  float interpolate_x[4];
  interpolate_x[0] = mix(beforeInterpolate[0], beforeInterpolate[1], steps.x);
  interpolate_x[1] = mix(beforeInterpolate[3], beforeInterpolate[2], steps.x);
  interpolate_x[2] = mix(beforeInterpolate[4], beforeInterpolate[5], steps.x);
  interpolate_x[3] = mix(beforeInterpolate[7], beforeInterpolate[6], steps.x);
  
  // interpolate point along y axis
  float interpolate_xy[2];
  interpolate_xy[0] = mix(interpolate_x[0], interpolate_x[1], steps.y);
  interpolate_xy[1] = mix(interpolate_x[2], interpolate_x[3], steps.y);

  //interpolate along z axis
  float interpolate_xyz;
  interpolate_xyz = mix(interpolate_xy[0], interpolate_xy[1], steps.z);

  return interpolate_xyz;
  /////////////////////////////////////////////////////////////////////////////
}

