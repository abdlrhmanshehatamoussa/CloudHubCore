using CloudHub.Domain.DTO;
using CloudHub.Domain.Models;

namespace CloudHub.Domain.Services
{
    public class EventsService : BaseService
    {
        public EventsService(IUnitOfWork unitOfWork, IEncryptionService encryptionService) : base(unitOfWork, encryptionService)
        {
        }

        public async Task<Event> LogSingle(ConsumerCredentials credentials, CreateEventDTO dto)
        {
            Consumer consumer = await GetConsumer(credentials);
            int tenantId = consumer.Client.TenantId;

            Event e = await _unitOfWork.EventsRepository.Add(Event.FromDTO(dto, tenantId));

            await ConsumeNonceOrThrow(consumer.Nonce.Id);

            await _unitOfWork.Save();

            return e;
        }

        public async Task LogBulk(ConsumerCredentials credentials, List<CreateEventDTO> dtos)
        {
            Consumer consumer = await GetConsumer(credentials);
            int tenantId = consumer.Client.TenantId;
            List<Event> events = dtos.Select(dto => Event.FromDTO(dto, tenantId)).ToList();
            await _unitOfWork.EventsRepository.SaveBulk(events);

            await ConsumeNonceOrThrow(consumer.Nonce.Id);
            await _unitOfWork.Save();
        }
    }
}
