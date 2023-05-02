#!/bin/bash

echo "Atualizar os pacotes do sistema"
apt-get update
apt-get update -y

echo "Instalar Apache 2 e Unzip"
apt-get install apache2 unzip -y

echo "Abrir o diretorio temporario, baixar arquivo e descompactar"
mkdir /tmp
cd /tmp
wget https://github.com/denilsonbonatti/linux-site-dio/archive/refs/heads/main.zip
unzip main.zip

echo "Copiar para a pasta do apache2"
cd linux-site-dio/
cp -R * /var/www/html/
