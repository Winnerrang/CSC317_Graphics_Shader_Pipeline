// Input:
//   N  3D unit normal vector
// Outputs:
//   T  3D unit tangent vector
//   B  3D unit bitangent vector
void tangent(in vec3 N, out vec3 T, out vec3 B)
{
  /////////////////////////////////////////////////////////////////////////////
  // Replace with your code 
  vec3 ref = vec3(1, 0, 0);
  
  T = cross(N, ref);
  B = cross(N, T);
  /////////////////////////////////////////////////////////////////////////////
}
