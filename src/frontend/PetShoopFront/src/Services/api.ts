import api from "../Services/Api";
import type {
  Cliente,
  CreateClienteDto,
  Pet,
  CreatePetDto,
  Funcionario,
  CreateFuncionarioDto,
  Produto,
  CreateProdutoDto,
  Servico,
  CreateServicoDto,
  Agendamento,
  CreateAgendamentoDto,
  Consulta,
  CreateConsultaDto,
  Vacina,
  CreateVacinaDto,
  Prontuario,
  CreateProntuarioDto,
  Venda,
  CreateVendaDto,
  ItemVenda,
  CreateItemVendaDto,
  LoginRequest,
  LoginResponse,
} from "../Types";

const getAuthHeaders = () => {
  const token = localStorage.getItem("accessToken");
  return token ? { Authorization: `Bearer ${token}` } : {};
};

export const authApi = {
  login: async (data: LoginRequest): Promise<LoginResponse> => {
    const response = await api.post("/v1/Auth/login", data);
    return response.data;
  },

  register: async (data: {
    userName: string;
    email: string;
    password: string;
  }): Promise<void> => {
    await api.post("/v1/Auth/register", data);
  },
};

export const clientesApi = {
  list: async (): Promise<Cliente[]> => {
    const response = await api.get("/v1/Clientes", {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  getById: async (id: string): Promise<Cliente> => {
    const response = await api.get(`/v1/Clientes/${id}`, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  create: async (data: CreateClienteDto): Promise<Cliente> => {
    const response = await api.post("/v1/Clientes", data, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  update: async (id: string, data: CreateClienteDto): Promise<Cliente> => {
    const response = await api.put(`/v1/Clientes/${id}`, data, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/v1/Clientes/${id}`, {
      headers: getAuthHeaders(),
    });
  },
};

export const petsApi = {
  list: async (): Promise<Pet[]> => {
    const response = await api.get("/v1/Pets", {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  getById: async (id: string): Promise<Pet> => {
    const response = await api.get(`/v1/Pets/${id}`, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  create: async (data: CreatePetDto): Promise<Pet> => {
    const response = await api.post("/v1/Pets", data, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  update: async (id: string, data: CreatePetDto): Promise<Pet> => {
    const response = await api.put(`/v1/Pets/${id}`, data, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/v1/Pets/${id}`, {
      headers: getAuthHeaders(),
    });
  },
};

export const funcionariosApi = {
  list: async (): Promise<Funcionario[]> => {
    const response = await api.get("/v1/Funcionarios", {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  getById: async (id: string): Promise<Funcionario> => {
    const response = await api.get(`/v1/Funcionarios/${id}`, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  create: async (data: CreateFuncionarioDto): Promise<Funcionario> => {
    const response = await api.post("/v1/Funcionarios", data, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  update: async (id: string, data: CreateFuncionarioDto): Promise<Funcionario> => {
    const response = await api.put(`/v1/Funcionarios/${id}`, data, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/v1/Funcionarios/${id}`, {
      headers: getAuthHeaders(),
    });
  },
};

export const produtosApi = {
  list: async (): Promise<Produto[]> => {
    const response = await api.get("/v1/Produtos", {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  getById: async (id: string): Promise<Produto> => {
    const response = await api.get(`/v1/Produtos/${id}`, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  create: async (data: CreateProdutoDto): Promise<Produto> => {
    const response = await api.post("/v1/Produtos", data, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  update: async (id: string, data: CreateProdutoDto): Promise<Produto> => {
    const response = await api.put(`/v1/Produtos/${id}`, data, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/v1/Produtos/${id}`, {
      headers: getAuthHeaders(),
    });
  },
};

export const servicosApi = {
  list: async (): Promise<Servico[]> => {
    const response = await api.get("/v1/Servicos", {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  getById: async (id: string): Promise<Servico> => {
    const response = await api.get(`/v1/Servicos/${id}`, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  create: async (data: CreateServicoDto): Promise<Servico> => {
    const response = await api.post("/v1/Servicos", data, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  update: async (id: string, data: CreateServicoDto): Promise<Servico> => {
    const response = await api.put(`/v1/Servicos/${id}`, data, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/v1/Servicos/${id}`, {
      headers: getAuthHeaders(),
    });
  },
};

export const agendamentosApi = {
  list: async (): Promise<Agendamento[]> => {
    const response = await api.get("/v1/Agendamentos", {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  getById: async (id: string): Promise<Agendamento> => {
    const response = await api.get(`/v1/Agendamentos/${id}`, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  create: async (data: CreateAgendamentoDto): Promise<Agendamento> => {
    const response = await api.post("/v1/Agendamentos", data, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  update: async (id: string, data: CreateAgendamentoDto): Promise<Agendamento> => {
    const response = await api.put(`/v1/Agendamentos/${id}`, data, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/v1/Agendamentos/${id}`, {
      headers: getAuthHeaders(),
    });
  },
};

export const consultasApi = {
  list: async (): Promise<Consulta[]> => {
    const response = await api.get("/v1/Consultas", {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  getById: async (id: string): Promise<Consulta> => {
    const response = await api.get(`/v1/Consultas/${id}`, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  create: async (data: CreateConsultaDto): Promise<Consulta> => {
    const response = await api.post("/v1/Consultas", data, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  update: async (id: string, data: CreateConsultaDto): Promise<Consulta> => {
    const response = await api.put(`/v1/Consultas/${id}`, data, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/v1/Consultas/${id}`, {
      headers: getAuthHeaders(),
    });
  },
};

export const vacinasApi = {
  list: async (): Promise<Vacina[]> => {
    const response = await api.get("/v1/Vacinas", {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  getById: async (id: string): Promise<Vacina> => {
    const response = await api.get(`/v1/Vacinas/${id}`, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  create: async (data: CreateVacinaDto): Promise<Vacina> => {
    const response = await api.post("/v1/Vacinas", data, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  update: async (id: string, data: CreateVacinaDto): Promise<Vacina> => {
    const response = await api.put(`/v1/Vacinas/${id}`, data, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/v1/Vacinas/${id}`, {
      headers: getAuthHeaders(),
    });
  },
};

export const prontuariosApi = {
  list: async (): Promise<Prontuario[]> => {
    const response = await api.get("/v1/Prontuarios", {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  getById: async (id: string): Promise<Prontuario> => {
    const response = await api.get(`/v1/Prontuarios/${id}`, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  create: async (data: CreateProntuarioDto): Promise<Prontuario> => {
    const response = await api.post("/v1/Prontuarios", data, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  update: async (id: string, data: CreateProntuarioDto): Promise<Prontuario> => {
    const response = await api.put(`/v1/Prontuarios/${id}`, data, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/v1/Prontuarios/${id}`, {
      headers: getAuthHeaders(),
    });
  },
};

export const vendasApi = {
  list: async (): Promise<Venda[]> => {
    const response = await api.get("/v1/Vendas", {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  getById: async (id: string): Promise<Venda> => {
    const response = await api.get(`/v1/Vendas/${id}`, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  create: async (data: CreateVendaDto): Promise<Venda> => {
    const response = await api.post("/v1/Vendas", data, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  update: async (id: string, data: CreateVendaDto): Promise<Venda> => {
    const response = await api.put(`/v1/Vendas/${id}`, data, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/v1/Vendas/${id}`, {
      headers: getAuthHeaders(),
    });
  },
};

export const itemVendasApi = {
  list: async (): Promise<ItemVenda[]> => {
    const response = await api.get("/v1/ItemVendas", {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  getById: async (id: string): Promise<ItemVenda> => {
    const response = await api.get(`/v1/ItemVendas/${id}`, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  create: async (data: CreateItemVendaDto): Promise<ItemVenda> => {
    const response = await api.post("/v1/ItemVendas", data, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  update: async (id: string, data: CreateItemVendaDto): Promise<ItemVenda> => {
    const response = await api.put(`/v1/ItemVendas/${id}`, data, {
      headers: getAuthHeaders(),
    });
    return response.data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/v1/ItemVendas/${id}`, {
      headers: getAuthHeaders(),
    });
  },
};
