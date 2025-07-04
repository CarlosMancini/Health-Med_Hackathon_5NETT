﻿using Core.Entities;
using Core.Inputs.AdicionarUsuario;
using Core.Inputs.Atualizar;
using Core.Inputs.Autenticar;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Utils.Enums;

namespace Core.Services
{
    public class UsuarioService : ServiceBase<Usuario>, IUsuarioService
    {
        private readonly ICriptografiaService _criptografiaService;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(
            ICriptografiaService criptografiaService,
            IUsuarioRepository usuarioRepository
        ) : base(usuarioRepository)
        {
            _criptografiaService = criptografiaService;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario?> AutenticarMedico(AutenticacaoMedicoInput input)
        {
            var usuario = await _usuarioRepository.AutenticarMedico(input);

            if (usuario is null || usuario.UsuarioSenha != input.Senha)
                return null;

            return usuario;
        }

        public async Task<Usuario?> AutenticarPaciente(AutenticacaoPacienteInput input)
        {
            var usuario = await _usuarioRepository.AutenticarPaciente(input);

            if (usuario is null || usuario.UsuarioSenha != input.Senha)
                return null;

            return usuario;
        }

        public async Task<Usuario> CadastrarUsuario(CadastrarUsuarioInput input, PerfilEnum perfil)
        {
            var emailJaExiste = await _usuarioRepository.ObterPorEmail(input.Email);
            if (emailJaExiste != null)
                throw new Exception("E-mail já cadastrado.");

            Usuario usuario = new Usuario
            {
                UsuarioNome = input.Nome,
                UsuarioEmail = input.Email,
                UsuarioCPF = input.UsuarioCPF,
                UsuarioSenha = _criptografiaService.Criptografar(input.Senha),
                PerfilId = (int)perfil,
                CriadoEm = DateTime.Now,
            };

            return await _usuarioRepository.CadastrarUsuario(usuario);
        }

        public async Task Atualizar(AtualizarUsuarioInput input, int usuarioLogadoId)
        {
            var usuario = await _usuarioRepository.ObterPorEmail(input.EmailAtual)
                ?? throw new Exception("Usuário não encontrado");

            if (usuario.Id != usuarioLogadoId)
                throw new Exception("Você só pode atualizar suas próprias credenciais.");

            var senhaAtualCriptografada = _criptografiaService.Criptografar(input.SenhaAtual);

            if (usuario.UsuarioSenha != senhaAtualCriptografada)
                throw new Exception("Senha atual incorreta.");

            usuario.UsuarioEmail = input.EmailNovo;
            usuario.UsuarioSenha = _criptografiaService.Criptografar(input.SenhaNova);

            await _usuarioRepository.Atualizar(usuario);
        }
    }
}
