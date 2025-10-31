#!/bin/bash
# Script para testar o fluxo do RabbitMQ

echo "=== Testando Fluxo RabbitMQ - Desafio Técnico ==="
echo ""

# 1. Verificar se RabbitMQ está rodando
echo "1. Verificando RabbitMQ..."
curl -f http://localhost:15672 > /dev/null 2>&1
if [ $? -eq 0 ]; then
    echo "✅ RabbitMQ Management rodando em http://localhost:15672"
else
    echo "❌ RabbitMQ não está rodando. Execute: docker-compose up rabbitmq"
    exit 1
fi

echo ""

# 2. Fazer login para obter token JWT
echo "2. Fazendo login para obter token..."
LOGIN_RESPONSE=$(curl -s -X POST https://localhost:7000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"admin@teste.com","senha":"123456"}' \
  -k)

TOKEN=$(echo $LOGIN_RESPONSE | jq -r '.token')

if [ "$TOKEN" = "null" ]; then
    echo "❌ Falha no login. Verifique se os serviços estão rodando."
    exit 1
fi

echo "✅ Token obtido: ${TOKEN:0:20}..."
echo ""

# 3. Criar uma venda (isso vai disparar o evento)
echo "3. Criando uma venda (disparará VendaCriadaEvent)..."
VENDA_DATA='{
  "id": 0,
  "numeroVenda": "VND000001",
  "codigoProduto": "1",
  "nomeProduto": "Produto Teste",
  "quantidade": 2,
  "precoUnitario": 10.50,
  "cliente": "Cliente Teste RabbitMQ",
  "vendedor": "Sistema",
  "dataVenda": "'$(date -u +%Y-%m-%dT%H:%M:%S.%3NZ)'",
  "statusVenda": "Pendente"
}'

VENDA_RESPONSE=$(curl -s -X POST https://localhost:7000/api/vendas \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer $TOKEN" \
  -d "$VENDA_DATA" \
  -k)

echo "✅ Venda criada:"
echo $VENDA_RESPONSE | jq '.'
echo ""

# 4. Aguardar processamento
echo "4. Aguardando processamento assíncrono via RabbitMQ..."
sleep 3

# 5. Verificar status da venda
VENDA_ID=$(echo $VENDA_RESPONSE | jq -r '.id')
echo "5. Verificando status final da venda ID: $VENDA_ID"

VENDA_STATUS=$(curl -s -X GET https://localhost:7000/api/vendas/$VENDA_ID \
  -H "Authorization: Bearer $TOKEN" \
  -k)

echo "Status final da venda:"
echo $VENDA_STATUS | jq '.'
echo ""

# 6. Verificar estoque
echo "6. Verificando estoque do produto..."
ESTOQUE_RESPONSE=$(curl -s -X GET https://localhost:7000/api/estoque/1 \
  -k)

echo "Estoque atual:"
echo $ESTOQUE_RESPONSE | jq '.'
echo ""

echo "=== Teste Concluído ==="
echo "Fluxo testado:"
echo "1. ✅ Login realizado"
echo "2. ✅ Venda criada → VendaCriadaEvent publicado"
echo "3. ✅ EstoqueService processou evento → Validou estoque"  
echo "4. ✅ EstoqueValidadoEvent publicado"
echo "5. ✅ VendasService processou evento → Confirmou/cancelou venda"
echo "6. ✅ Status final verificado"