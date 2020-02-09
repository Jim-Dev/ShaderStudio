#version 330 core
layout (location = 0) in vec3 aVertPosition;
layout(location = 1) in vec3 aVertColor;
layout (location = 2) in vec3 aVertNormal;
layout (location = 3) in vec2 aTexCoord;

uniform mat4 inverse_model;
uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

out vec3 FragPosition;
out vec2 TexCoord;
out vec3 VertexColor;  
out vec3 VertexNormal;

void main()
{
	// note that we read the multiplication from right to left
    gl_Position = projection * view * model * vec4(aVertPosition, 1.0);
    FragPosition = vec3(model*vec4(aVertPosition, 1.0));
	VertexColor = aVertColor;
    VertexNormal = mat3(transpose(inverse_model)) * aVertNormal; 
    TexCoord = vec2(aTexCoord.x, aTexCoord.y);
}