export interface Cliente {
  clienteId: string;
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
  petId: string;
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
  clienteNome?: string;
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
  clienteNome?: string;
}

export interface Funcionario {
  funcionarioId: string;
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
  produtoId: string;
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
  servicoId: string;
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
  agendamentoId: string;
  petId: string;
  servicoId: string;
  funcionarioId: string;
  dataHora: string;
  status: string;
  observacoes: string;
  petNome?: string;
  servicoNome?: string;
  funcionarioNome?: string;
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
  consultaId: string;
  petId: string;
  funcionarioId: string;
  dataConsulta: string;
  peso: number;
  temperatura: number;
  diagnostico: string;
  prescricao: string;
  petNome?: string;
  funcionarioNome?: string;
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
  vacinaId: string;
  petId: string;
  nome: string;
  fabricante: string;
  dataAplicacao: string;
  proximaDose: string;
  petNome?: string;
}

export interface CreateVacinaDto {
  petId: string;
  nome: string;
  fabricante: string;
  dataAplicacao: string;
  proximaDose: string;
}

export interface Prontuario {
  prontuarioId: string;
  petId: string;
  funcionarioId: string;
  dataRegistro: string;
  descricao: string;
  petNome?: string;
  funcionarioNome?: string;
}

export interface CreateProntuarioDto {
  petId: string;
  funcionarioId: string;
  dataRegistro: string;
  descricao: string;
}

export interface Venda {
  vendaId: string;
  clienteId: string;
  dataVenda: string;
  valorTotal: number;
  formaPagamento: string;
  clienteNome?: string;
}

export interface CreateVendaDto {
  clienteId: string;
  dataVenda: string;
  valorTotal: number;
  formaPagamento: string;
}

export interface ItemVenda {
  itemVendaId: string;
  vendaId: string;
  produtoId: string;
  quantidade: number;
  valorUnitario: number;
  produtoNome?: string;
  vendaInfo?: string;
  clienteNome?: string;
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

export interface Dashboard {
  totalClientes: number;
  totalPets: number;
  totalFuncionarios: number;
  totalProdutos: number;
  totalAgendamentos: number;
  totalVendas: number;
  receitaTotal: number;
  agendamentosHoje: number;
  agendamentosPendentes: number;
}
