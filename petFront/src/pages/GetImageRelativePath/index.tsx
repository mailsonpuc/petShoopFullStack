import { api } from "../../services/Api";



// Função para obter a URL completa de uma imagem, dado um caminho relativo.
// O caminho relativo, por exemplo, poderia ser "img/51HvjwRCBJL._AC_SL1000_.jpg"
// A função concatena a URL base da API com o caminho relativo, garantindo que a URL seja válida.

// Parâmetro:
// relativePath - string que representa o caminho relativo da imagem no backend.

//pegar a imagem da pasta do backend, caminho completo
//http://localhost:5297/img/51HvjwRCBJL._AC_SL1000_.jpg

export function getFullImageUrl(relativePath: string) {
  // Retorna a URL completa da imagem ao concatenar a baseURL da API com o caminho relativo.
  // '.replace(/^\/+/, '')' remove quaisquer barras '/' no início do caminho relativo,
  // para evitar problemas de barras duplicadas na URL.
  return `${api.defaults.baseURL}/${relativePath.replace(/^\/+/, '')}`;
}



// - Caminho relativo:  
//   img/imagem.jpg  

// - Caminho absoluto:  
//   http://localhost:5297/img/imagem.jpg  