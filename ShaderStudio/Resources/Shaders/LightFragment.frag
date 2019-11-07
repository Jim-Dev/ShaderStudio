#version 330 core
out vec4 FragColor;

uniform vec4 LightColor;
uniform float LightIntensity;

void main()
{
    FragColor = LightColor * LightIntensity; // set all 4 vector values to 1.0
}