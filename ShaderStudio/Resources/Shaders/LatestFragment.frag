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
#version 330 core

#define L_MAX_DIRECTIONAL 2
#define L_MAX_POINT 4
#define L_MAX_SPOT 2

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

struct Material {
	float shininess;
	vec3 diffuse;
	vec3 specular;
};
struct PointLight {    
    vec3 position;
    float intensity;

    float constant;
    float linear;
    float quadratic;  

    vec3 diffuse;
    vec3 specular;
};  

struct DirLight {
    vec3 direction;
  
  	float intensity;
    vec3 diffuse;
    vec3 specular;
};  
struct SpotLight {
    vec3 position;
    vec3 direction;
  
  	float intensity;
    vec3 diffuse;
    vec3 specular;
    float cutoff;
    float outerCutoff;
};  

uniform DirLight DirLights[L_MAX_DIRECTIONAL];
uniform PointLight PointLights[L_MAX_POINT];
uniform SpotLight SpotLights[L_MAX_SPOT];

Material material;

float GetDiffuseLightContribution(vec3 normal, vec3 lightDir)
{
    return max(dot(normal, lightDir), 0.0);
}
float GetSpecularLightContribution(vec3 normal, vec3 lightDir, vec3 viewDir)
{
    vec3 reflectDir = reflect(-lightDir, normal);
    return pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
}
vec3 CalcPointLight(PointLight light, vec3 normal, vec3 fragPos, vec3 viewDir)
{
    vec3 lightDir = normalize(light.position - fragPos);
    float diff = GetDiffuseLightContribution(normal, lightDir);
    float spec = GetSpecularLightContribution(normal,lightDir,viewDir);

    // attenuation
    float distance    = length(light.position - fragPos);
    float attenuation = 1.0 / (light.constant + light.linear * distance + 
  			     light.quadratic * (distance * distance));    
    // combine results

    vec3 diffuse  = light.diffuse  * diff * material.diffuse * light.intensity;
    vec3 specular = light.specular * spec * material.specular* light.intensity;

    diffuse  *= attenuation;
    specular *= attenuation;
    return ( diffuse + specular);
} 

vec3 CalcDirLight(DirLight light, vec3 normal,vec3 viewDir)
{
    vec3 lightDir = normalize(-light.direction);
    float diff = GetDiffuseLightContribution(normal, lightDir);

    vec3 diffuse  = light.diffuse  * diff * material.diffuse*light.intensity;
    return ( diffuse );
}  
vec3 CalcSpotLight(SpotLight light, vec3 normal,vec3 fragPos, vec3 viewDir)
{
    vec3 lightDir = normalize(light.position - fragPos);
    float diff = GetDiffuseLightContribution(normal, lightDir);
    float spec = GetSpecularLightContribution(normal,lightDir,viewDir);

    float theta = dot(viewDir,normalize(-light.direction));
    theta = dot(lightDir,normalize(-light.direction));
    float epsilon = light.cutoff - light.outerCutoff;
    float intensity = clamp((theta-light.outerCutoff)/epsilon,0.0,1.0);

    vec3 diffuse = light.diffuse * diff * material.diffuse * intensity;
    vec3 specular = light.specular * spec * material.specular * intensity;

    return vec3( diffuse);
}

void main()
{
	material.shininess = 64;
	material.diffuse = vec3(texture(_TEX0, TexCoord));
	material.specular = vec3(texture(_TEX1, TexCoord));

	vec3 norm = normalize(VertexNormal);
	vec3 viewDir = normalize(CameraPosition-FragPosition);

	vec3 ambientLight = L_AmbientColor*L_AmbientIntensity;

	vec3 directionalLights;
  	for(int i = 0; i < L_MAX_DIRECTIONAL; i++)
    	directionalLights += CalcDirLight(DirLights[i], norm,viewDir);  

	vec3 pointLights;
  	for(int i = 0; i < L_MAX_POINT; i++)
    	pointLights += CalcPointLight(PointLights[i], norm, FragPosition, viewDir);  

    vec3 spotLights;
    for(int i = 0; i < L_MAX_SPOT; i++)
    	spotLights += CalcSpotLight(SpotLights[i], norm,  FragPosition,viewDir);  

	vec3 result = ambientLight+pointLights+directionalLights+spotLights;
	FragColor = vec4(result,1);
}
