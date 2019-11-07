#version 330 core
out vec4 FragColor;

in vec3 VertexColor;
in vec2 TexCoord;
uniform float Time;

uniform sampler2D _TEX0;
uniform sampler2D _TEX1;
uniform sampler2D _TEX2;
uniform sampler2D _TEX3;
uniform sampler2D _TEX4;
uniform sampler2D _TEX5;
uniform sampler2D _TEX6;
uniform sampler2D _TEX7;

void main()
{
	vec4 output;
	if (sin(Time)<0)
		output = mix(texture(_TEX0, TexCoord),texture(_TEX2, TexCoord),abs(sin(Time)));
	else
		output = mix(texture(_TEX0, TexCoord),vec4(VertexColor,1),abs(sin(Time)));
	FragColor = vec4(output.rgb,1);
}