#version 330 core

layout (location = 0) in vec3 aVertPosition;
layout(location = 1) in vec3 aVertColor;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

out vec3 VertexColor;  

void main()
{
	VertexColor = aVertColor;
    gl_Position = projection * view * model * vec4(aVertPosition, 1.0);
} 