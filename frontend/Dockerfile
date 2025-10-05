# Etapa 1: Build da aplicação
FROM node:24-alpine3.21 AS builder

# Diretório de trabalho
WORKDIR /app

# Copiar package.json e package-lock.json
COPY package*.json ./

# Instalar dependências
RUN npm install

# Copiar todo o código da aplicação
COPY . .

# Build da aplicação
RUN npm run build

# Etapa 2: Servir com Nginx
FROM nginx:alpine

# Remover configuração default
RUN rm /etc/nginx/conf.d/default.conf

# Copiar configuração customizada do Nginx
COPY docker/nginx.conf /etc/nginx/conf.d/

# Copiar build da aplicação
COPY --from=builder /app/dist /usr/share/nginx/html

# Expor porta
EXPOSE 80

# Comando padrão
CMD ["nginx", "-g", "daemon off;"]
