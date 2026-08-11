export interface Cliente {
  id: string;
  nome: string;
  cpf: string;
  email: string;
  telefone: string;
  dataDeNascimento: string;
  endereco: string;
}

export interface CreateClienteDto {
  nome: string;
  cpf: string;
  email: string;
  telefone: string;
  dataDeNascimento: string;
  endereco: string;
}

export interface Pet {
  id: string;
  nome: string;
  especie: string;
  raca: string;
  sexo: string;
  dataDeNascimento: string;
  peso: number;
  cor: string;
  porte: string;
  observacoes: string;
  clienteId: string;
}

export interface CreatePetDto {
  nome: string;
  especie: string;
  raca: string;
  sexo: string;
  dataDeNascimento: string;
  peso: number;
  cor: string;
  porte: string;
  observacoes: string;
  clienteId: string;
}

export interface Funcionario {
  id: string;
  nome: string;
  cpf: string;
  email: string;
  telefone: string;
  cargo: string;
  salario: number;
  dataAdmissao: string;
}

export interface CreateFuncionarioDto {
  nome: string;
  cpf: string;
  email: string;
  telefone: string;
  cargo: string;
  salario: number;
  dataAdmissao: string;
}

export interface Produto {
  id: string;
  nome: string;
  descricao: string;
  categoria: string;
  marca: string;
  preco: number;
  quantidadeEmEstoque: number;
}

export interface CreateProdutoDto {
  nome: string;
  descricao: string;
  categoria: string;
  marca: string;
  preco: number;
  quantidadeEmEstoque: number;
}

export interface Servico {
  id: string;
  nome: string;
  descricao: string;
  preco: number;
  duracaoEmMinutos: number;
}

export interface CreateServicoDto {
  nome: string;
  descricao: string;
  preco: number;
  duracaoEmMinutos: number;
}

export interface Agendamento {
  id: string;
  petId: string;
  servicoId: string;
  funcionarioId: string;
  dataHora: string;
  status: string;
  observacoes: string;
}

export interface CreateAgendamentoDto {
  petId: string;
  servicoId: string;
  funcionarioId: string;
  dataHora: string;
  status: string;
  observacoes: string;
}

export interface Consulta {
  id: string;
  petId: string;
  funcionarioId: string;
  dataConsulta: string;
  peso: number;
  temperatura: number;
  diagnostico: string;
  prescricao: string;
}

export interface CreateConsultaDto {
  petId: string;
  funcionarioId: string;
  dataConsulta: string;
  peso: number;
  temperatura: number;
  diagnostico: string;
  prescricao: string;
}

export interface Vacina {
  id: string;
  petId: string;
  nome: string;
  fabricante: string;
  dataAplicacao: string;
  proximaDose: string;
}

export interface CreateVacinaDto {
  petId: string;
  nome: string;
  fabricante: string;
  dataAplicacao: string;
  proximaDose: string;
}

export interface Prontuario {
  id: string;
  petId: string;
  funcionarioId: string;
  dataRegistro: string;
  descricao: string;
}

export interface CreateProntuarioDto {
  petId: string;
  funcionarioId: string;
  dataRegistro: string;
  descricao: string;
}

export interface Venda {
  id: string;
  clienteId: string;
  dataVenda: string;
  valorTotal: number;
  formaPagamento: string;
}

export interface CreateVendaDto {
  clienteId: string;
  dataVenda: string;
  valorTotal: number;
  formaPagamento: string;
}

export interface ItemVenda {
  id: string;
  vendaId: string;
  produtoId: string;
  quantidade: number;
  valorUnitario: number;
}

export interface CreateItemVendaDto {
  vendaId: string;
  produtoId: string;
  quantidade: number;
  valorUnitario: number;
}

export interface LoginRequest {
  userName: string;
  password: string;
}

export interface LoginResponse {
  accessToken: string;
  refreshToken: string;
  userName: string;
  email: string;
  roles: string[];
}

export interface ApiError {
  message: string;
  statusCode: number;
}
