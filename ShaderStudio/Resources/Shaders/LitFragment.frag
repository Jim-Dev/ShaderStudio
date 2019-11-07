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

uniform vec3 L_Ambient;

void main()
{
	vec4 output = texture(_TEX0,TexCoord) * vec4(L_Ambient,1);
	FragColor = vec4(mix(output.rgb,VertexColor.rgb,0.25),1);
}
