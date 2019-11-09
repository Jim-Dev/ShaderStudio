#version 330 core
out vec4 FragColor;

in vec3 VertexColor;
in vec3 VertexNormal;
in vec2 TexCoord;
in vec3 FragPosition;
uniform float Time;

uniform sampler2D _TEX0;
uniform sampler2D _TEX1;
uniform sampler2D _TEX2;
uniform sampler2D _TEX3;
uniform sampler2D _TEX4;
uniform sampler2D _TEX5;
uniform sampler2D _TEX6;
uniform sampler2D _TEX7;

uniform vec3 CameraPosition;

uniform vec3 L_AmbientColor;
uniform float L_AmbientIntensity;

uniform vec3 L_SimpleLightColor;
uniform float L_SimpleLightIntensity;
uniform vec3 L_Position;

void main()
{
	vec3 simpleLight = L_SimpleLightColor*L_SimpleLightIntensity;
	vec3 ambient = L_AmbientColor*(L_AmbientIntensity/10);

	vec3 norm = normalize(VertexNormal);
	vec3 lightDir = normalize(L_Position - FragPosition);
	float diff = max(dot(norm,lightDir),0.0);
	vec3 diffuse = diff*(simpleLight/2);

	float specularStrength = 1.2;
	vec3 viewDir = normalize(CameraPosition-FragPosition);
	vec3 reflectDir = reflect(-lightDir,norm);
	float spec = pow(max(dot(viewDir,reflectDir),0.0),32);
	vec3 specular = specularStrength*spec*(simpleLight/2);

	FragColor = vec4(ambient+diffuse+specular,1)*texture(_TEX0,TexCoord);
}