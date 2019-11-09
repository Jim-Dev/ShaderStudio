#version 330 core
out vec4 FragColor;

uniform vec3 LightColor;
uniform float LightIntensity;

void main()
{
    FragColor = vec4(LightColor * LightIntensity,1); // set all 4 vector values to 1.0
}