import React from 'react';

const HelloWorld = ({ name = "Mundo" }) => {
  const currentDate = new Date().toLocaleDateString('pt-BR');
  
  return (
    <div style={{
      padding: '20px',
      margin: '20px',
      border: '2px solid #61dafb',
      borderRadius: '10px',
      backgroundColor: '#282c34',
      color: 'white'
    }}>
      <h2>Olá, {name}!</h2>
      <p>Este é um componente React funcional.</p>
      <p>Data de hoje: {currentDate}</p>
      <ul style={{ textAlign: 'left', maxWidth: '300px', margin: '0 auto' }}>
        <li>✅ Projeto React criado</li>
        <li>✅ Componente funcional</li>
        <li>✅ Props implementadas</li>
        <li>✅ Estado dinâmico (data)</li>
      </ul>
    </div>
  );
};

export default HelloWorld;