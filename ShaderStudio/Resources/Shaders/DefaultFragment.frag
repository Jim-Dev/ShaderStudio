#version 330 core
out vec4 FragColor;

in vec3 VertexColor;
in vec2 TexCoord;

uniform sampler2D _TEX0;
uniform sampler2D _TEX1;

void main()
{
	vec4 output = mix(texture(_TEX0, TexCoord), texture(_TEX1, TexCoord), 0.2);
	FragColor = vec4(mix(output.rgb,VertexColor.rgb,0.25),1);
}
