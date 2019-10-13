#version 330 core
out vec4 FragColor;

in vec3 VertexColor;
in vec2 TexCoord;

uniform sampler2D texBackground;
uniform sampler2D texForeground;

void main()
{
	vec4 output = mix(texture(texBackground, TexCoord), texture(texForeground, TexCoord), 0.2);
	FragColor = vec4(mix(output.rgb,VertexColor.rgb,0.25),1);
}
