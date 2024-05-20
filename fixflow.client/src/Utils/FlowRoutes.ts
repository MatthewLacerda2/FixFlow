export const FLOW_ROUTES = {
  root: "/",
  none: "*",
  business: {
    root: "/e",
    agendamentos: "/agendamentos",
    atendimentos: "/atendimentos",
    contatos: "/contatos",
    infographics: "/infographics",
    settings: "/settings",
    preferences: "/preferences",
  },
  clients: {
    root: "/in",
    agendamentos: "/agendamentos",
    atendimentos: "/atendimentos",
    settings: "/settings",
    preferences: "/preferences",
  },
  agendamento: {
    id: "/*",
  },
  reset: {
    root: "/reset",
    request: "/request",
    reset: "/*",
  },
};
